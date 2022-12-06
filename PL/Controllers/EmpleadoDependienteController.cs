using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public IActionResult GetAllEmpleado()
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

        [HttpGet]
        public IActionResult GetAllDependienteById(string NumeroEmpleado)
        {
            ML.Result resultEmpleado = BL.Empleado.GetById(NumeroEmpleado);

            if (resultEmpleado.Correct && resultEmpleado.Object != null)
            {
                ML.Result resultDependiente = BL.Dependiente.GetByNumeroEmpleado(NumeroEmpleado);
                ViewBag.Message = resultDependiente.Message;

                ML.Dependiente dependiente = new ML.Dependiente();

                dependiente.Empleado = (Empleado)resultEmpleado.Object;

                if (resultDependiente.Correct)
                {
                    dependiente.Dependientes = resultDependiente.Objects;
                }
                return View(dependiente);
            }
            else
            {
                return Redirect("/EmpleadoDependiente/GetAllEmpleado");
            }

        }

        [HttpGet]
        public ActionResult Form(int? IdDependiente)
        {
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();
            ML.Result resultEstadoCivil = BL.EstadoCivil.GetAll();

            ML.Dependiente dependiente = new ML.Dependiente();

            if (IdDependiente == null)
            {
                // Pasamos esos parámetros a nuestros objetos
                dependiente.DependienteTipo = new ML.DependienteTipo();
                dependiente.EstadoCivil = new ML.EstadoCivil();

                dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                dependiente.EstadoCivil.EstadosCiviles = resultEstadoCivil.Objects;
                // ADD
                return View(dependiente);
            }
            else
            {
                ML.Result result = BL.Dependiente.GetById(IdDependiente.Value);
                ViewBag.Message = result.Message;
                if (result.Correct && result.Object != null)
                {
                    // Igualamos el objeto usuario con lo obtenido en la consulta
                    dependiente = (ML.Dependiente)result.Object;

                    dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                    dependiente.EstadoCivil.EstadosCiviles = resultEstadoCivil.Objects;

                    return View(dependiente);
                }
            }
            return Redirect("/EmpleadoDependiente/GetAllEmpleado");
            //
        }

        //[HttpPost]
        //public ActionResult Form(ML.Empresa empresa)
        //{

        //    if (empresa != null && ModelState.IsValid)
        //    {
        //        // ADD
        //        if (empresa.IdEmpresa == 0)
        //        {
        //            if (empresa != null)
        //            {
        //                ML.Result result = BL.Empresa.Add(empresa);
        //                ViewBag.Message = result.Message;
        //                if (result.Correct)
        //                {
        //                    ModelState.Clear();
        //                }
        //            }
        //        }
        //        else
        //        // UPDATE
        //        {
        //            if (empresa != null)
        //            {
        //                ML.Result result = BL.Empresa.Update(empresa);
        //                ViewBag.Message = result.Message;
        //                if (result.Correct)
        //                {
        //                    ModelState.Clear();
        //                }
        //            }
        //        }

        //    }


        //    return PartialView("Modal");
        //}

    }
}
