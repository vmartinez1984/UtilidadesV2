namespace Peliculas.Bl
{
    public class PeliculaBl
    {
        private readonly PeliculaRepositorio _repositorio;
        private readonly AlmacenDeArchivos _almacenDeArchivos;

        public PeliculaBl(
            PeliculaRepositorio repositorio,
            AlmacenDeArchivos almacenDeArchivos
        )
        { _repositorio = repositorio; _almacenDeArchivos = almacenDeArchivos; }

        public async Task ActualizarAsync(int id, PeliculaDtoIn pelicula)
        {
            Pelicula pelicula1;

            pelicula1 = await _repositorio.ObtenerAsync(id);
            pelicula1.Titulo = pelicula.Titulo;
            pelicula1.FechaDeVista = pelicula.FechaDeVista;
            pelicula1.Resumen = pelicula.Resumen;
            pelicula1.Trailer = pelicula.Trailer;
            if (pelicula.Poster is not null)
            {
                string ruta;

                ruta = await _almacenDeArchivos.ActualizarArchivoAsync(pelicula1.NombreDelArchivo, pelicula.Poster);

                pelicula1.Poster = ruta;
            }
            await _repositorio.ActualizarAsync(pelicula1);
        }

        public async Task<int> AgregarAsync(PeliculaDtoIn pelicula)
        {
            int id;
            string rutaDelArchivo;
            Pelicula peliculaEntidad;

            peliculaEntidad = pelicula.ToEntity();
            if (pelicula.Poster != null)
            {
                string aliasDelArchivo;

                aliasDelArchivo = $"{Guid.NewGuid()}{Path.GetExtension(pelicula.Poster.FileName)}";
                rutaDelArchivo = await _almacenDeArchivos.Guardar(aliasDelArchivo, pelicula.Poster);
                peliculaEntidad.Poster = rutaDelArchivo;
                peliculaEntidad.AliasDelArchivo = aliasDelArchivo;
                peliculaEntidad.NombreDelArchivo = pelicula.Poster.FileName;
                peliculaEntidad.Extension = pelicula.Poster.ContentType;
            }
            id = await _repositorio.AgregarAsync(peliculaEntidad);

            return id;
        }


        public async Task<List<PeliculaDto>> ObtenerAsync() => (await _repositorio.ObtenerAsync()).ToDtos();

        public async Task<PeliculaDto> ObtenerAsync(int id) => (await _repositorio.ObtenerAsync(id)).ToDto();

        public async Task<byte[]> ObtenerImagenAsync(int id)
        {
            Pelicula pelicula;
            byte[] bytes;

            pelicula = await _repositorio.ObtenerAsync(id);
            bytes = await _almacenDeArchivos.ObtenerBytes(pelicula.Poster);

            return bytes;
        }
    }

    internal static class Mapeador
    {
        internal static PeliculaDto ToDto(this Pelicula entity) => entity is null ? null : new PeliculaDto
        {
            FechaDeRegistro = entity.FechaDeRegistro,
            FechaDeVista = entity.FechaDeVista,
            Id = entity.Id,
            Resumen = entity.Resumen,
            Titulo = entity.Titulo,
            Trailer = entity.Trailer
        };

        internal static List<PeliculaDto> ToDtos(this List<Pelicula> entities) => entities.Select(ToDto).ToList();

        internal static Pelicula ToEntity(this PeliculaDtoIn pelicula) => pelicula is null ? null : new Pelicula
        {
            FechaDeRegistro = DateTime.Now,
            Resumen = pelicula.Resumen,
            Titulo = pelicula.Titulo,
            Trailer = pelicula.Trailer,
        };
    }
}
