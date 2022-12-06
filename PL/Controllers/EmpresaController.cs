using Microsoft.AspNetCore.Mvc;
using ML;
using System.Drawing.Drawing2D;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            Empresa business = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll(business);

            ViewBag.Message = result.Message;
            if (result.Correct && result.Objects.Count > 0)
            {
                ML.Empresa empresa = new ML.Empresa();
                empresa.Empresas = result.Objects;
                return View(empresa);
            }
            return View("/");
        }

        [HttpPost]
        public IActionResult GetAll(ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.GetAll(empresa);

            ViewBag.Message = result.Message;
            if (result.Correct /*&& result.Objects.Count > 0*/)
            {
                empresa = new ML.Empresa();
                empresa.Empresas = result.Objects;
            }
            return View(empresa);
        }

        //[HttpDelete]
        public ActionResult Delete(int? IdEmpresa)
        {
            if (IdEmpresa == null)
            {
                return Redirect("/Empresa/GetAll");
            }
            else
            {
                ML.Result result = BL.Empresa.Delete(IdEmpresa.Value);
                ViewBag.Message = result.Message;
                if (result.Correct)
                {
                    // return Redirect("/Usuario/GetAll");
                }
            }
            return PartialView("Modal");
        }


        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {

            // Instanciamos un objeto alumno
            ML.Empresa empresa = new ML.Empresa();

            // Inicializamos los objetos que hacen referencia a la información del usuario.


            if (IdEmpresa == null)
            {
                // Pasamos esos parámetros a nuestros objetos
                // ADD
                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);
                ViewBag.Message = result.Message;
                if (result.Correct && result.Object != null)
                {
                    // Igualamos el objeto usuario con lo obtenido en la consulta
                    empresa = (ML.Empresa)result.Object;
                    return View(empresa);
                }
                return Redirect("/Empresa/GetAll");
            }
            //
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {

            IFormFile logo = Request.Form.Files["logo"];

            if (empresa != null && ModelState.IsValid)
            {
                // Verificamos si se envió imagen
                if (logo != null && logo.Length > 0)
                {
                    // Guardamos la imagen que ingreso el usuario y la pasamos a Base64 String
                    empresa.Logo = ConvertToBase64String(logo);
                }
                // ADD
                if (empresa.IdEmpresa == 0)
                {
                    if (empresa != null)
                    {
                        ML.Result result = BL.Empresa.Add(empresa);
                        ViewBag.Message = result.Message;
                        if (result.Correct)
                        {
                            ModelState.Clear();
                        }
                    }
                }
                else
                // UPDATE
                {
                    if (empresa != null)
                    {
                        ML.Result result = BL.Empresa.Update(empresa);
                        ViewBag.Message = result.Message;
                        if (result.Correct)
                        {
                            ModelState.Clear();
                        }
                    }
                }

            }


            return PartialView("Modal");
        }
        public static string ConvertToBase64String(IFormFile image)
        {

            using var fileStream = image.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return Convert.ToBase64String(bytes);

        }
    }
}
