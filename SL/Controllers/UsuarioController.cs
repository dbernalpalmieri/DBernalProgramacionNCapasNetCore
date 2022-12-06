using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();

            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.Message);
            }
        }

        // GET api/<UsuarioController>/5
        [HttpGet("GetById/{idUsuario}")]
        public IActionResult Get(int idUsuario)
        {
            if (idUsuario != 0)
            {
                ML.Result result = BL.Usuario.GetById(idUsuario);
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

        [HttpPost("GetAllParam")]
        public IActionResult Post(string? nombre, string? apellidoPaterno, byte idRol)
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new Rol();
            usuario.Nombre = nombre;
            usuario.ApellidoPaterno = apellidoPaterno;
            usuario.Rol.IdRol = idRol;  
            
            ML.Result result = BL.Usuario.GetAll(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost("GetAll")]
        public IActionResult PostGetAll(ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }


        [HttpPost("Add")]
        public IActionResult Post([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.Add(usuario);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPut("Update")]
        public IActionResult Put([FromBody]  ML.Usuario usuario)
        {
            if (usuario.IdUsuario != null && usuario.IdUsuario != 0)
            {
                ML.Result result = BL.Usuario.Update(usuario);
                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(result.Message);
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

        [HttpPut("UpdateStatus/{idUsuario}")]
        public IActionResult Put(int idUsuario)
        {
            if (idUsuario != null && idUsuario != 0)
            {
                ML.Result result = BL.Usuario.UpdateStatus(idUsuario);
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

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{idUsuario}")]
        public IActionResult Delete(int idUsuario)
        {
            if (idUsuario != null && idUsuario != 0)
            {
                ML.Result result = BL.Usuario.Delete(idUsuario);
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
    
    
