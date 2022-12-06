using Microsoft.AspNetCore.Mvc;
using ML;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace PL.Controllers
{



    public class UsuarioController : Controller
    {
        // IConfiguration nos permite acceder a valores establecidos en nuestro appsettings.json
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public UsuarioController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        //private readonly HttpClient httpClient;

        //public UsuarioController(HttpClient httpClient)
        //{
        //    this.httpClient = httpClient;
        //}

        [HttpGet]
        public IActionResult GetAll()
        {
            //ML.Usuario user = new ML.Usuario();
            //user.Rol = new ML.Rol();

            //ML.Result result = BL.Usuario.GetAll(user);
            //Result resultRoles = BL.Rol.GetAll();

            //ViewBag.Message = result.Message;

            //if (result.Correct && result.Objects.Count > 0 && resultRoles.Correct)
            //{
            //    //IEnumerable<ModelLayer.Usuario> usuarios = result.Objects.OfType<ModelLayer.Usuario>().ToList();
            //    ML.Usuario usuario = new ML.Usuario();
            //    usuario.Rol = new ML.Rol();
            //    usuario.Rol.Roles = resultRoles.Objects;

            //    usuario.Usuarios = result.Objects;
            //    return View(usuario);
            //}
            //return View("/");
            // 15 101

            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string urlAPI = _configuration["UrlAPI"];
                    httpClient.BaseAddress = new Uri(urlAPI);


                    //Sending request to find web api REST service resource using HttpClient
                    var request = httpClient.GetAsync("Usuario/GetAll");
                    request.Wait();

                    //Checking the response is successful or not which is sent using HttpClient
                    var response = request.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = response.Content.ReadAsStringAsync().Result;

                        ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                        result.Message = resultAPI.Message;

                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in resultAPI.Objects)
                            {
                                ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                                result.Objects.Add(user);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                result.Correct = false;
                result.Message = error.Message;
            }


            ViewBag.Message = result.Message;

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            Result resultRoles = BL.Rol.GetAll();
            usuario.Rol.Roles = resultRoles.Objects;

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult GetAll(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.GetAll(usuario);
            //Result resultRoles = BL.Rol.GetAll();
            //ViewBag.Message = result.Message;
            //if (result.Correct && result.Objects.Count > 0 && resultRoles.Correct)
            //{
            //    //IEnumerable<ModelLayer.Usuario> usuarios = result.Objects.OfType<ModelLayer.Usuario>().ToList();
            //    usuario = new ML.Usuario();
            //    usuario.Rol = new ML.Rol();
            //    usuario.Rol.Roles = resultRoles.Objects;

            //    usuario.Usuarios = result.Objects;
            //    return View(usuario);
            //}
            //return View("/");

            ML.Result result = new ML.Result();
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    string urlAPI = _configuration["UrlAPI"];
                    httpClient.BaseAddress = new Uri(urlAPI);


                    //Sending request to find web api REST service resource using HttpClient
                    var request = httpClient.PostAsJsonAsync<ML.Usuario>("Usuario/GetAll", usuario);
                    request.Wait();

                    //Checking the response is successful or not which is sent using HttpClient
                    var response = request.Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var readContent = response.Content.ReadAsStringAsync().Result;

                        ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                        result.Message = resultAPI.Message;

                        if (resultAPI.Correct)
                        {
                            result.Objects = new List<object>();
                            foreach (var item in resultAPI.Objects)
                            {
                                ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(item.ToString());
                                result.Objects.Add(user);
                            }
                            result.Correct = true;
                        }
                    }
                }
            }
            catch (Exception error)
            {
                result.Correct = false;
                result.Message = error.Message;
            }


            ViewBag.Message = result.Message;
            Result resultRoles = BL.Rol.GetAll();

            usuario.Rol = new ML.Rol();
            usuario.Rol.Roles = resultRoles.Objects;

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }

            return View("/");


        }

        //[HttpDelete]
        public ActionResult Delete(int? IdUsuario)
        {
            if (IdUsuario == null)
            {
                return Redirect("/Usuario/GetAll");
            }
            else
            {
                //ML.Result result = BL.Usuario.Delete(IdUsuario.Value);
                //ViewBag.Message = result.Message;
                //if (result.Correct)
                //{
                //    // return Redirect("/Usuario/GetAll");
                //}
                ML.Result result = new ML.Result();
                try
                {
                    using (HttpClient httpClient = new HttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                        string urlAPI = _configuration["UrlAPI"];
                        httpClient.BaseAddress = new Uri(urlAPI);


                        //Sending request to find web api REST service resource using HttpClient
                        var request = httpClient.DeleteAsync($"Usuario/Delete/{IdUsuario}");
                        request.Wait();

                        //Checking the response is successful or not which is sent using HttpClient
                        var response = request.Result;
                        if (response.IsSuccessStatusCode)
                        {
                            var readContent = response.Content.ReadAsStringAsync().Result;

                            ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                            result.Message = resultAPI.Message;

                            if (resultAPI.Correct)
                            {
                                result.Correct = true;
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    result.Correct = false;
                    result.Message = error.Message;
                }


                ViewBag.Message = result.Message;

            }
            return PartialView("Modal");
        }




        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            Result resultRoles = BL.Rol.GetAll();
            Result resultPaises = BL.Pais.GetAll();


            if (resultRoles.Correct && resultPaises.Correct)
            {
                // Instanciamos un objeto alumno
                ML.Usuario usuario = new ML.Usuario();

                // Inicializamos los objetos que hacen referencia a la información del usuario.
                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                if (IdUsuario == null)
                {
                    // Pasamos esos parámetros a nuestros objetos
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                    usuario.Rol.Roles = resultRoles.Objects;
                    // ADD

                }
                else
                {
                    //ML.Result result = BL.Usuario.GetById(IdUsuario.Value);
                    //ViewBag.Message = result.Message;
                    // GET BY ID
                    ML.Result result = new ML.Result();
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Clear();
                            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            string urlAPI = _configuration["UrlAPI"];
                            httpClient.BaseAddress = new Uri(urlAPI);


                            //Sending request to find web api REST service resource using HttpClient
                            var request = httpClient.GetAsync($"Usuario/GetById/{IdUsuario.Value}");
                            request.Wait();

                            //Checking the response is successful or not which is sent using HttpClient
                            var response = request.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var readContent = response.Content.ReadAsStringAsync().Result;

                                ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                                result.Message = resultAPI.Message;

                                if (resultAPI.Correct)
                                {
                                    ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(resultAPI.Object.ToString());
                                    result.Object = user;
                                    result.Correct = true;
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        result.Correct = false;
                        result.Message = error.Message;
                    }


                    ViewBag.Message = result.Message;



                    if (result.Correct && result.Object != null)
                    {
                        // Igualamos el objeto usuario con lo obtenido en la consulta
                        usuario = (ML.Usuario)result.Object;

                        // Volvemos a pasar el listado de elementos que componen al objeto Usuario ya que se borraron en la igualación
                        usuario.Rol.Roles = resultRoles.Objects;
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                        // Obtenemos algunos elementos 
                        Result resultEstados = BL.Estado.GetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                        Result resultMunicipios = BL.Municipio.GetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                        Result resultColonias = BL.Colonia.GetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);
                        // Los igualamos a la parte del usuario que corresponde
                        usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                        usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                        usuario.Direccion.Colonia.Colonias = resultColonias.Objects;

                    }
                }
                return View(usuario);

            }
            return Redirect("/Usuario/GetAll");
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {

            //ModelState.ClearValidationState("NombreCompleto");
            //ModelState.MarkFieldValid("NombreCompleto");

            var errors = ModelState.Values.SelectMany(x => x.Errors);
            if (usuario != null && ModelState.IsValid)
            {
                IFormFile image = Request.Form.Files["image"];
                // Verificamos si se envió imagen
                if (image != null && image.Length > 0)
                {
                    // Guardamos la imagen que ingreso el usuario y la pasamos a Base64 String
                    usuario.Imagen = ConvertToBase64String(image);
                }


                // ADD
                if (usuario.IdUsuario == 0)
                {

                    //ML.Result result = BL.Usuario.Add(usuario);
                    //ViewBag.Message = result.Message;
                    //if (result.Correct)
                    //{
                    //    ModelState.Clear();
                    //}
                    ML.Result result = new ML.Result();
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Clear();
                            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            string urlAPI = _configuration["UrlAPI"];
                            httpClient.BaseAddress = new Uri(urlAPI);


                            //Sending request to find web api REST service resource using HttpClient
                            var request = httpClient.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                            request.Wait();

                            //Checking the response is successful or not which is sent using HttpClient
                            var response = request.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var readContent = response.Content.ReadAsStringAsync().Result;

                                ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                                result.Message = resultAPI.Message;

                                if (resultAPI.Correct)
                                {
                                    ML.Usuario user = JsonConvert.DeserializeObject<ML.Usuario>(resultAPI.Object.ToString());
                                    result.Object = user;
                                    result.Correct = true;
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        result.Correct = false;
                        result.Message = error.Message;
                    }
                    ViewBag.Message = result.Message;

                }
                else
                // UPDATE
                {
                    //ML.Result result = BL.Usuario.Update(usuario);
                    //ViewBag.Message = result.Message;
                    //if (result.Correct)
                    //{

                    //    ModelState.Clear();
                    //}
                    ML.Result result = new ML.Result();
                    try
                    {
                        using (HttpClient httpClient = new HttpClient())
                        {
                            httpClient.DefaultRequestHeaders.Clear();
                            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                            string urlAPI = _configuration["UrlAPI"];
                            httpClient.BaseAddress = new Uri(urlAPI);


                            //Sending request to find web api REST service resource using HttpClient
                            var request = httpClient.PutAsJsonAsync<ML.Usuario>("Usuario/Update", usuario);
                            request.Wait();

                            //Checking the response is successful or not which is sent using HttpClient
                            var response = request.Result;
                            if (response.IsSuccessStatusCode)
                            {
                                var readContent = response.Content.ReadAsStringAsync().Result;

                                ML.Result resultAPI = JsonConvert.DeserializeObject<ML.Result>(readContent);

                                result.Message = resultAPI.Message;

                                if (resultAPI.Correct)
                                {
                                    result.Correct = true;
                                }
                            }
                        }
                    }
                    catch (Exception error)
                    {
                        result.Correct = false;
                        result.Message = error.Message;
                    }
                    ViewBag.Message = result.Message;
                }
            }
            else
            {
                Result resultRoles = BL.Rol.GetAll();
                Result resultPaises = BL.Pais.GetAll();

                usuario.Rol = new ML.Rol();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                usuario.Rol.Roles = resultRoles.Objects;
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                ViewBag.Message = "La información dada está incompleta.";
                return View(usuario);
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


        public JsonResult GetEstados(int IdPais)
        {
            ML.Result result = BL.Estado.GetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult GetMunicipios(int IdEstado)
        {
            ML.Result result = BL.Municipio.GetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult GetColonias(int IdMunicipio)
        {
            ML.Result result = BL.Colonia.GetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }
        public JsonResult ChangeStatus(int IdUsuario)
        {
            ML.Result result = BL.Usuario.UpdateStatus(IdUsuario);
            return Json(result);
        }

        //public static string ConvertToBase64String(IFormFile image)
        //{
        //    using (var memoeryStream = new MemoryStream())
        //    {
        //        image.CopyTo(memoeryStream);
        //        var fileBytes = memoeryStream.ToArray();
        //        return Convert.ToBase64String(fileBytes);
        //    }
        //}

    }
}
