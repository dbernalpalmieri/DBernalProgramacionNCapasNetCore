using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColoniaController : ControllerBase
    {
        [HttpGet("GetByIdMunicipio")]
        public IActionResult GetByIdMunicipio(ML.Municipio municipio)
        {
            if (municipio.IdMunicipio != 0)
            {
                ML.Result result = BL.Colonia.GetByIdMunicipio(municipio.IdMunicipio);
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
