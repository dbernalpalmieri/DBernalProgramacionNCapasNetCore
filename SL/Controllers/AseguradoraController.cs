using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AseguradoraController : ControllerBase
    {
        // GET: api/<AseguradoraController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        // GET api/<AseguradoraController>/5
        [HttpGet("GetById/{idAseguradora}")]
        public IActionResult Get(int idAseguradora)
        {
            if (idAseguradora != 0)
            {
                ML.Result result = BL.Aseguradora.GetById(idAseguradora);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result.Message);
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

        // POST api/<AseguradoraController>
        [HttpPost("Add")]
        public IActionResult Post(ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // PUT api/<AseguradoraController>/5
        [HttpPut("Update")]
        public IActionResult Put(ML.Aseguradora aseguradora)
        {
            if (aseguradora.IdAseguradora != null && aseguradora.IdAseguradora != 0)
            {
                ML.Result result = BL.Aseguradora.Update(aseguradora);
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

        // DELETE api/<AseguradoraController>/5
        [HttpDelete("Delete/{idAseguradora}")]
        public IActionResult Delete(int idAseguradora)
        {
            if (idAseguradora != null && idAseguradora != 0)
            {
                ML.Result result = BL.Aseguradora.Delete(idAseguradora);
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
