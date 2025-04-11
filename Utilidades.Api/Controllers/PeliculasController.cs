using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Peliculas.Bl;

namespace Utilidades.Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
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
        public async Task<IActionResult> ObtnerAsync()
        {
            List<PeliculaDto> peliculas;

            peliculas = await _pelicula.ObtenerAsync();

            return Ok(peliculas);
        }
    }
}
