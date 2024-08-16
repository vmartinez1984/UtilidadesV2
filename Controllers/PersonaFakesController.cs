using Microsoft.AspNetCore.Mvc;
using utilidadesv2.Dtos;
using utilidadesv2.Servicios;

namespace utilidadesv2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaFakesController : ControllerBase
    {
        private readonly ServicioDePersona _servicioDePersona;

        public PersonaFakesController(ServicioDePersona servicioDePersona)
        {
            _servicioDePersona = servicioDePersona;
        }
        /// <summary>
        /// Genera una persona fake con datos aleatorios
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> ObtenerPersonaFake(int numeroDePersonas = 1)
        {
            if (numeroDePersonas == 1)
            {
                PersonaFakeDto personaFake;

                personaFake = await _servicioDePersona.ObtenerAsync();

                return Ok(personaFake);
            }

            List<PersonaFakeDto> lista = new List<PersonaFakeDto>();

            for (int i = 0; i < numeroDePersonas; i++)
            {
                PersonaFakeDto personaFake;

                personaFake = await _servicioDePersona.ObtenerAsync();
                lista.Add(personaFake);
            }

            return Ok(lista);
        }

        /// <summary>
        /// Inserta nombre y apellidos en la base de datos
        /// </summary>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<ActionResult> AgregarNombreYApellidos()
        //{
        //   await _servicioDePersona.AgregarNombreYApellidosAsync();

        //   return Ok();
        //}
    }
}