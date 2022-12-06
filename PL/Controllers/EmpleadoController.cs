using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
    {
        public IActionResult GetAll()
        {
            ML.Result result = BL.Empleado.GetAll();
            ViewBag.Message = result.Message;

            ML.Empleado empleado = new ML.Empleado();
            if (result.Correct)
            {
                empleado.Empleados = result.Objects;
            }
            return View(empleado);
        }

        //[HttpDelete]
        public ActionResult Delete(string? NumeroEmpleado)
        {
            if (NumeroEmpleado == null)
            {
                return Redirect("/Empleado/GetAll");
            }
            else
            {
                ML.Result result = BL.Empleado.Delete(NumeroEmpleado);
                ViewBag.Message = result.Message;
                if (result.Correct)
                { 

                }
            }
            return PartialView("Modal");
        }

        [HttpGet]
        public ActionResult Form(string? NumeroEmpleado)
        {
            ML.Empresa empresa= new ML.Empresa();
            Result resultEmpresas = BL.Empresa.GetAll(empresa);

            // Instanciamos un objeto empleado
            ML.Empleado empleado = new ML.Empleado();

            if (resultEmpresas.Correct)
            {
                

                // Inicializamos los objetos que hacen referencia a la información del usuario.
                empleado.Empresa = new ML.Empresa();

                // ADD
                if (NumeroEmpleado == null)
                {
                    // Pasamos esos parámetros a nuestros objetos
                    empleado.Empresa.Empresas = resultEmpresas.Objects;
                    empleado.Action = "ADD";
                   

                }
                else
                // UPDATE
                {
                    ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                    ViewBag.Message = result.Message;
                    if (result.Correct && result.Object != null)
                    {
                        // Igualamos el objeto usuario con lo obtenido en la consulta
                        empleado = (ML.Empleado)result.Object;
                        empleado.Action = "UDPATE";
                        // Volvemos a pasar el listado de elementos que componen al objeto Usuario ya que se borraron en la igualación
                        empleado.Empresa.Empresas = resultEmpresas.Objects;

                        // Obtenemos algunos elementos 
                    }
                    else
                    {
                        return Redirect("/Empleado/GetAll");
                    }
                }
            }
            return View(empleado);

        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            if (empleado != null)
            {
                IFormFile image = Request.Form.Files["image"];
                // Verificamos si se envió imagen
                if (image != null && image.Length > 0)
                {
                    // Guardamos la imagen que ingreso el empleado y la pasamos a Base64 String
                    empleado.Foto = ConvertToBase64String(image);
                }
                ML.Result result = BL.Empleado.GetById(empleado.NumeroEmpleado);
                // UPDATE
                if (empleado.Action.Equals("UDPATE")/*result.Correct && result.Object != null*/)
                {
                    result = BL.Empleado.Update(empleado);
                    ViewBag.Message = result.Message;
                    if (result.Correct)
                    {

                        ModelState.Clear();
                    }
                }
                // ADD
                if (empleado.Action.Equals("ADD"))
                {
                    result = BL.Empleado.Add(empleado);
                    ViewBag.Message = result.Message;
                    if (result.Correct)
                    {
                        ModelState.Clear();
                    }

                }
            }
            else
            {
                ViewBag.Message = "La información dada está incompleta.";
                return View(empleado);
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
