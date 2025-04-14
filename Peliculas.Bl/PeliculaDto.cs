using Microsoft.AspNetCore.Http;

namespace Peliculas.Bl
{
    public class PeliculaDtoIn
    {
        public IFormFile? Poster { get; set; }

        public string Titulo { get; set; }

        public string Resumen { get; set; }

        public string? Trailer { get; set; }

        public DateTime? FechaDeVista { get;  set; }
    }

    public class PeliculaDto
    {
        public int Id { get; set; }

        public DateTime FechaDeRegistro { get; set; }

        public DateTime? FechaDeVista { get; set; }

        public string Titulo { get; set; }

        public string Resumen { get; set; }

        public string Trailer { get; set; }
    }
}
