using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios;


namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Provides API endpoints for generating valid fake CLABE account numbers according to Mexican banking standards.
    /// </summary>
    /// <remarks>This controller is part of API version 1 and is intended for use in scenarios where a valid
    /// CLABE number is required for testing or demonstration purposes. The controller depends on a CLABE service, which
    /// is injected via constructor. All endpoints are accessible under the 'api/Clabes' route.</remarks>
    [ApiController]
    [Route("api/[controller]")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ClabesController : ControllerBase
    {
        private readonly ClabeServicio _servicio;

        /// <summary>
        /// Contructor del controlador de clabes, se inyecta el servicio de clabes
        /// </summary>
        /// <param name="servicio"></param>
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

            return Created("", new { Clabe = clabe, Fecha = DateTime.Now });
        }
    }
}