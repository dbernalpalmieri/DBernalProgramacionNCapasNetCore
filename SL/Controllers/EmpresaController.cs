using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase
    {
        // GET: api/<EmpresaController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Empresa empresa = new ML.Empresa();

            ML.Result result = BL.Empresa.GetAll(empresa);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }


        // GET api/<EmpresaController>/5
        [HttpGet("GetById/{idEmpresa}")]
        public IActionResult GetById(int idEmpresa)
        {
            if (idEmpresa != 0)
            {

                ML.Result result = BL.Empresa.GetById(idEmpresa);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            else
            {
                return BadRequest(new
                {
                    correct = false,
                    message = "Especifica el Id del objeto a buscar"
                });
            }
        }


        [HttpPost("GetAll")]
        public IActionResult PostGetAll([FromBody] ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.GetAll(empresa);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }



        // POST api/<EmpresaController>
        [HttpPost("Add")]
        public IActionResult PostAdd([FromBody]ML.Empresa empresa)
        {
            ML.Result result = BL.Empresa.Add(empresa);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<EmpresaController>/5
        [HttpPut("Update")]
        public IActionResult Put([FromBody]ML.Empresa empresa)
        {
            if (empresa.IdEmpresa != null && empresa.IdEmpresa != 0)
            {
                ML.Result result = BL.Empresa.Update(empresa);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {

                return BadRequest(new
                {
                    correct = false,
                    message = "Especifica el Id del objeto a actualizar"
                });
            }
               
        }

        // DELETE api/<EmpresaController>/5
        [HttpDelete("Delete/{idEmpresa}")]
        public IActionResult Delete(int idEmpresa)
        {
            if (idEmpresa != null && idEmpresa != 0)
            {
                ML.Result result = BL.Empresa.Delete(idEmpresa);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result);
                }
            }
            else
            {
                return BadRequest(new
                {
                    correct = false,
                    message = "Especifica el Id del objeto a eliminar"
                });
            }
          
        }
    }
}
