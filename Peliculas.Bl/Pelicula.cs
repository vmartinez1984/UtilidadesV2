namespace Peliculas.Bl
{
    public class Pelicula
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Resumen { get; set; }

        public string Poster { get; set; }

        public string Extension { get; set; }

        public string Trailer { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public DateTime? FechaDeVista { get; set; }

    }
}
