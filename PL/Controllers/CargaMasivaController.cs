using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using ML;
using Newtonsoft.Json;
using System.Data;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        // IConfiguration nos permite acceder a valores establecidos en nuestro appsettings.json
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }


        [HttpGet]
        public ActionResult CargaMasiva()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {

            List<string> listError = new List<string>();

            IFormFile fileTXT = Request.Form.Files["txt_file"];
            // Verificamos si se envió imagen
            if (fileTXT != null && fileTXT.Length > 0)
            {
                StreamReader TextFile = new StreamReader(fileTXT.OpenReadStream());
                string line;

                line = TextFile.ReadLine();
                while ((line = TextFile.ReadLine()) != null)
                {
                    try
                    {
                        string[] lines = line.Split('|');

                        ML.Usuario usuario = new ML.Usuario();

                        usuario.Nombre = lines[0];
                        usuario.ApellidoPaterno = lines[1];
                        usuario.ApellidoMaterno = lines[2];
                        usuario.FechaNacimiento = lines[3];
                        usuario.Email = lines[4];
                        usuario.Sexo = char.Parse(lines[5].Trim());
                        usuario.Password = lines[6];
                        usuario.UserName = lines[7];
                        usuario.Telefono = lines[8];
                        usuario.Celular = lines[9];
                        usuario.Curp = lines[10];
                        usuario.Imagen = null;

                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = byte.Parse(lines[11]);

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.Calle = lines[12];
                        usuario.Direccion.NumeroExterior = lines[13];
                        usuario.Direccion.NumeroInterior = lines[14];

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

                        ML.Result result = BL.Usuario.Add(usuario);

                        if (result.Correct)
                        {

                        }
                        else
                        {
                            // Crear .txt de errores
                            listError.Add(result.Message);
                        }
                    }
                    catch (Exception error)
                    {
                        listError.Add($"Mensaje: {error.Message}");
                    }
                }
                if(listError.Count > 0) 
                {
                    ML.Result resultErrors = new ML.Result();
                    resultErrors.Message = $"Hubo un total de {listError.Count} errores al tratar de insertar el fichero .txt en la base de datos.";
                    resultErrors.Objects = listError.OfType<object>().ToList();
                    resultErrors.Correct = false;
                    return View("CargaMasiva", resultErrors);
                 
                }
                else
                {
                    ViewBag.Message = "Toda la información se agregado con éxito en la Base de Datos";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "No se ha cargado ningún .txt";
            }

            return View();
        }
        public FileResult DownloadErrorFile(string JSONresult)
        {
            ML.Result result = JsonConvert.DeserializeObject<ML.Result>(JSONresult);

            using (MemoryStream stream = new MemoryStream())
            {
                StreamWriter objStreamWriter = new StreamWriter(stream);

                objStreamWriter.WriteLine("File of errors");
                objStreamWriter.WriteLine($"Created at: {DateTime.Now.ToString()}");
                objStreamWriter.WriteLine("Author: DBP");
                objStreamWriter.WriteLine("List of errors ! ");
                result.Objects.ForEach(error =>
                {
                    objStreamWriter.WriteLine(error);
                });

                objStreamWriter.Flush();
                objStreamWriter.Close();
                return File(stream.ToArray(), "text/plain", "errorLog.txt");
            }
        }

        [HttpPost]
        public ActionResult CargaXLSX()
        {
            IFormFile fileXLSX = Request.Form.Files["xls_file"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                // Validamos que el usuario mando un fichero
                if (fileXLSX != null && fileXLSX.Length > 0)
                {
                    // Del fichero mandado por el usuario obtenemos su nombre y extension del archivo
                    string fileName = Path.GetFileName(fileXLSX.FileName);
                    string extensionArchivo = Path.GetExtension(fileXLSX.FileName).ToLower();

                    // Ahora extraemos algunos valores almacenados en nuestro appsettings.json
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionModulo = _configuration["TipoExcel"];

                    // Validamos que fichero del usuario tenga una extension permitida
                    if (extensionArchivo == extensionModulo)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, $"{Path.GetFileNameWithoutExtension(fileName)}-{DateTime.Now.ToString("dd-MM-yyyy HH_mm_ss")}.xlsx");

                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                fileXLSX.CopyTo(stream);    
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                            ML.Result resultDataTable = BL.Usuario.ConvertXSLXtoDataTable(connectionString);
                            if (resultDataTable.Correct)
                            {
                                // DataTable tableEmpleado = (DataTable)resultDataTable.Object; //unboxing
                                ML.Result resultValidarExcel = BL.Usuario.ValidarExcel(resultDataTable.Objects);
                                if (resultValidarExcel.Objects.Count == 0) 
                                {
                                    ViewBag.Message = "Todos los registros han sido validados correctamente, puede proceder a cargarlos.";
                                    resultValidarExcel.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo",filePath);

                                    return View("CargaMasiva", resultValidarExcel);

                                }
                                else    // Si hubo errores
                                {
                                    return View("CargaMasiva", resultValidarExcel);
                                }
                            }
                            else
                            {
                                ViewBag.Message = resultDataTable.Message;
                                return PartialView("Modal");
                            }

                        }
                    }
                    else
                    {
                        ViewBag.Message = "El fichero cargado no tiene una extension .xlsx";
                    }
                }else
                {
                    ViewBag.Message = "Selecciona un archivo .xlsx";
                }
            }
            else
            {

                // Empezamos ahora si con la inserción de los datos
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertXSLXtoDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario itemUsuario in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(itemUsuario);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add($"No se insertó el Usuario {itemUsuario.Nombre} - Error: {resultAdd.Message}");
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        ML.Result resultErrors = new ML.Result();
                        resultErrors.Message = $"Hubo un total de {resultErrores.Objects.Count} errores al tratar de insertar el fichero .xlsx en la base de datos.";
                        resultErrors.Objects = resultErrores.Objects;
                        resultErrors.Correct = false;
                        return View("CargaMasiva", resultErrors);
                    }
                    else
                    {
                        ViewBag.Message = "Todos los Usuarios han sido registrados correctamente";
                    }
                    HttpContext.Session.Remove("PathArchivo");
                }
            }
            return PartialView("Modal");
        }

    }
}

/*
 * 
Emmott|Harrie|Laise|13/01/1982|elaise7@tinypic.com|M|l1q7iOsE|elaise7|4043111907|7102987574|SSDE095195TPWVRF39|1|Lindbergh|85409|69890|340
Celinka|Bentame|Bowering|08/08/1982|cbowering8@miitbeian.gov.cn|F|pigByY9luQ|cbowering8|7823096340|6517338344|WWMY826683WHXRMG94|3|Commercial|17736|32551|7686
Emlyn|Condell|Pinnion|13/04/1989|epinnion9@booking.com|M|OwKhZF|epinnion9|3932213123|9785748164|LYXC656928EGCTYZ68|3|Dayton|827|45086|6453
Nickolaus|Sear|Calfe|05/08/1986|ncalfea@sphinn.com|M|dAzx3aK7|ncalfea|3037089663|7366520040|WGBY854985YOPYXO18|1|Briar Crest|071|4511|4
Salli|Gorgl|Capelle|26/06/1980|scapelleb@smugmug.com|F|Vhd5edhik|scapelleb|7957320351|9985870447|OLMJ856915GXVMYD69|1|Bobwhite|37112|37|25115
Gilles|Dodimead|Lerwill|22/06/1987|glerwillc@hexun.com|M|YyyYwh|glerwillc|8273427841|5746063798|UEFT331767ZGHDFH84|1|Shopko|28616|2151|6
Nevins|Astling|Di Dello|27/12/1983|ndidellod@pinterest.com|M|YSqVIHl|ndidellod|4387557306|4615898288|OWIV281081TBUHGT58|2|Carpenter|03936|2|356
Barr|Parlour|Redbourn|07/08/1997|bredbourne@newsvine.com|M|CeDAc09|bredbourne|2635724802|3925351689|PQSA192924JKVWXF78|2|Karstens|2|5151|11544
Arabel|Joanic|Wickey|03/03/1988|awickeyf@washingtonpost.com|F|YpTLhjS|awickeyf|2792104153|4708925134|GRRD604043WOZCUG69|1|Butternut|18613|153|659
Nicolis|Vondrach|Durward|05/03/1987|ndurwardg@mayoclinic.com|M|8r0AIVLGufQu|ndurwardg|7553278080|3449316056|FQSY373353PJJAHO88|2|Miller|802|68093|5491
Colette|Antowski|Delahunty|13/04/1990|cdelahuntyh@squidoo.com|F|Qy1X70rlNs|cdelahuntyh|6494585956|1131450117|SKXA831753EGNSIB13|2|Melby|98396|3|05
Grady|Christy|Boise|27/03/1983|gboisei@reuters.com|M|wYFYedLmqs|gboisei|9484436831|5476667607|JJPA923667NDAXKN02|3|Grover|032|58822|4
Iosep|Aynsley|Moffet|29/10/1995|imoffetj@biglobe.ne.jp|M|PzmQGBq|imoffetj|2503605141|1896327968|YMNM228462KCDTCD47|3|Cambridge|45|57128|13918
 * 
 * 
 * 
 */