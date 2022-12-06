﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipioController : ControllerBase
    {
        [HttpGet("GetByIdEstado")]
        public IActionResult GetByIdEstado(ML.Estado estado)
        {
            if (estado.IdEstado != 0)
            {
                ML.Result result = BL.Municipio.GetByIdEstado(estado.IdEstado);
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
