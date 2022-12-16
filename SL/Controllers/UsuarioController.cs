using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ML;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Object GeneratereToken(ML.Usuario usuario)
        {
            var jwt = _configuration.GetSection("JWT").Get<ML.JWT>();
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim("id", usuario.IdUsuario.ToString()),
            new Claim("email", usuario.Email),
            new Claim("username", usuario.UserName)

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var singIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    jwt.Issuer,
                    jwt.Audience,
                    claims,
                    expires: DateTime.Now.AddDays(4),
                    signingCredentials: singIn
                );

            return new
            {
                success = true,
                message= "Éxito",
                result = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }


        // GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            //var identity = HttpContext.User.Identities as ClaimsIdentity;

            //ML.Result resultValidate = BL.Usuario.ValidarToken(identity);

            //if (!resultValidate.Correct) return Unauthorized(resultValidate);

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
        [HttpGet("GetByUsernameEmail/{UsernameEmail}")]
        public IActionResult GetByUsernameEmail(string UsernameEmail)
        {
            if (!UsernameEmail.IsNullOrEmpty())
            {
                ML.Result result = BL.Usuario.GetByUsernameEmail(UsernameEmail);
                if (result.Correct)
                {
                    ML.Usuario usuario = (Usuario)result.Object;
                    return Ok(GeneratereToken(usuario));
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
                    message = "Especifica el parámetro del objeto a buscar"
                });
            }

        }

        [HttpGet("GetById/{idUsuario}")]
        public IActionResult GetById(int idUsuario)
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

/*
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
 
 
 */


