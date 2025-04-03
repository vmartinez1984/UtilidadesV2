using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios.Dtos;
using Utilidades.Servicios;

namespace Utilidades.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DireccionesController : ControllerBase
    {
        private readonly PersonaFakeServicio _servicioDePersona;
        private readonly DireccionServicio direccionServicio;

        public DireccionesController(
            PersonaFakeServicio servicioDePersona,
            DireccionServicio direccionServicio
        )
        {
            _servicioDePersona = servicioDePersona;
            this.direccionServicio = direccionServicio;
        }

        /// <summary>
        /// Otiene una dirección valida con coordenadas, es una colección de direcciones de bibliotecas en México
        /// </summary>
        /// <returns></returns>        
        [HttpGet("Direcciones")]
        public async Task<ActionResult> ObtenerDirecciones(int numeroDeDirecciones = 1)
        {
            if (numeroDeDirecciones == 1)
            {
                DireccionDto direccion;

                direccion = await direccionServicio.OtenerAleatorio();

                return Ok(direccion);
            }

            List<DireccionDto> lista = new List<DireccionDto>();

            for (int i = 0; i < numeroDeDirecciones; i++)
            {
                DireccionDto direccion;

                direccion = await direccionServicio.OtenerAleatorio();
                lista.Add(direccion);
            }

            return Ok(lista);
        }
    }
}
