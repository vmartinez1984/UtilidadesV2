using Microsoft.AspNetCore.Mvc;
using utilidadesv2.Servicios;

namespace utilidadesv2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClabesController : ControllerBase
    {
        private readonly ServicioDeClabes _servicio;

        public ClabesController(ServicioDeClabes servicio)
        {
            _servicio = servicio;
        }

        /// <summary>
        /// Genera un cuenta clabe fake valida con los estandadres de México
        /// </summary>
        /// <param name="ahorroId"></param>
        /// <returns></returns>
        [HttpGet("{ahorroId}")]
        public IActionResult Get(string ahorroId)
        {
            string clabe;

            clabe = _servicio.GenerarClabe(ahorroId);

            return Created("", new { Clabe = clabe });
        }
    }
}