using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
            ViewBag.Message = result.Message;

            ML.Aseguradora aseguradora = new ML.Aseguradora();
            if (result.Correct /*&& result.Objects.Count >0*/)
            {
                aseguradora.Aseguradoras = result.Objects;
            }
            return View(aseguradora);
        }

        // [HttpDelete]
        public ActionResult Delete(int? IdAseguradora)
        {
            if (IdAseguradora == null)
            {
                return Redirect("/Aseguradora/GetAll");
            }
            else
            {
                ML.Result result = BL.Aseguradora.Delete(IdAseguradora.Value);
                ViewBag.Message = result.Message;
                if (result.Correct)
                {
                    // return Redirect("/Usuario/GetAll");
                }
            }
            return PartialView("Modal");
        }





        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
        {
            // Instanciamos un objeto 
            ML.Aseguradora aseguradora = new ML.Aseguradora();

            // Inicializamos los objetos que hacen referencia a la información que le complementa.
            ML.Usuario user = new ML.Usuario();
            ML.Result resultUsuarios = BL.Usuario.GetAll(user);

            aseguradora.Usuario = new ML.Usuario();

            if (IdAseguradora == null)
            {
                // Pasamos esos parámetros a nuestros objeto
                
                aseguradora.Usuario.Usuarios = resultUsuarios.Objects;
                // ADD
                return View(aseguradora);
            }
            else
            {
                ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);
                ViewBag.Message = result.Message;
                if (result.Correct && result.Object != null)
                {
                    // Igualamos el objeto con lo obtenido en la consulta
                    aseguradora = (ML.Aseguradora)result.Object;
                    // Pasamos esos parámetros a nuestros objeto
                    aseguradora.Usuario.Usuarios = resultUsuarios.Objects;

                    return View(aseguradora);
                }
                return Redirect("/Aseguradora/GetAll");
            }
        }
        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            // ADD
            if (aseguradora.IdAseguradora == 0)
            {
                if (aseguradora != null)
                {
                    ML.Result result = BL.Aseguradora.Add(aseguradora);
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
                if (aseguradora != null)
                {
                    ML.Result result = BL.Aseguradora.Update(aseguradora);
                    ViewBag.Message = result.Message;
                    if (result.Correct)
                    {
                        ModelState.Clear();
                    }
                }
            }
            return PartialView("Modal");
        }
    }
}
