using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios;


namespace Utilidades.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClabesController : ControllerBase
    {
        private readonly ClabeServicio _servicio;

        public ClabesController(ClabeServicio servicio)
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