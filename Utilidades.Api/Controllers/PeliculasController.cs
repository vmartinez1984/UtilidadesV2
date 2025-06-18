using Microsoft.AspNetCore.Mvc;
using Peliculas.Bl;

namespace Utilidades.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    //[EndpointGroupName("YoMerengues")]
    public class PeliculasController : ControllerBase
    {
        private readonly PeliculaBl _pelicula;

        public PeliculasController(PeliculaBl pelicula)
        {
            _pelicula = pelicula;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarAsync([FromForm] PeliculaDtoIn pelicula)
        {
            int id;

            id = await _pelicula.AgregarAsync(pelicula);

            return Ok(new { id });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObtnerAsync(int id)
        {
            PeliculaDto peliculas;

            peliculas = await _pelicula.ObtenerAsync(id);
            if (peliculas is null)
                return NotFound(new { Mensaje = "Nel carnal, no te la vengo manejando" });
            return Ok(peliculas);
        }

        [HttpGet("{id}/imagenes")]
        public async Task<IActionResult> ObetnerImagenAsync(int id)
        {
            byte[] bytes;

            bytes = await _pelicula.ObtenerImagenAsync(id);

            return File(bytes, "image/png");
        }

        [HttpGet]
        public async Task<IActionResult> ObtnerAsync(bool? vista = null)
        {
            List<PeliculaDto> peliculas;

            peliculas = await _pelicula.ObtenerAsync();
            if(vista is not null)
            {
                if (vista == true)
                    peliculas = peliculas.Where(x => x.FechaDeVista != null).ToList();
                else
                    peliculas = peliculas.Where(x => x.FechaDeVista == null).ToList();
            }

            return Ok(peliculas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarAsync(int id, [FromForm] PeliculaDtoIn pelicula)
        {
            await _pelicula.ActualizarAsync(id, pelicula);

            return Accepted();
        }

        [HttpPut("{id}/Vistas/{vista}")]
        public async Task<IActionResult> ActualizarAsync(int id, bool vista)
        {
            PeliculaDto peliculas;

            peliculas = await _pelicula.ObtenerAsync(id);
            if (peliculas is null)
                return NotFound(new { Mensaje = "Nel carnal, no te la vengo manejando" });

            await _pelicula.MarcarAsync(id, vista);

            return Accepted();
        }
    }
}
