using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        [HttpGet("GetByIdPais")]
        public IActionResult GetByIdPais(ML.Pais pais)
        {
            if (pais.IdPais != 0)
            {

                ML.Result result = BL.Estado.GetByIdPais(pais.IdPais);
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
    }
}
