using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios;
using Utilidades.Servicios.Dtos;

namespace Utilidades.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonasFakesController : ControllerBase
    {
        private readonly PersonaFakeServicio _servicioDePersona;
        private readonly DireccionServicio direccionServicio;

        public PersonasFakesController(
            PersonaFakeServicio servicioDePersona,
            DireccionServicio direccionServicio
        )
        {
            _servicioDePersona = servicioDePersona;
            this.direccionServicio = direccionServicio;
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
    }
    
   
}