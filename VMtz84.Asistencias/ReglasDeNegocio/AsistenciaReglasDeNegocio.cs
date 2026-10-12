using VMtz84.Asistencias.Dtos;
using VMtz84.Asistencias.Entities;
using VMtz84.Asistencias.Repositorios;
using VMtz84.Asistencias.Servicios;

namespace VMtz84.Asistencias.ReglasDeNegocio
{
    public class AsistenciaReglasDeNegocio
    {
        private readonly AsistenciaRepositorio _repositorio;
        private readonly AlmancenDeArchivosServicio _almancenDeArchivos;
        const string contenedor = "asistencias";
        public AsistenciaReglasDeNegocio(AsistenciaRepositorio repositorio, AlmancenDeArchivosServicio almancenDeArchivos)
        {
            this._repositorio = repositorio;
            this._almancenDeArchivos = almancenDeArchivos;
        }

        public async Task<int> AgregarAsync(AsistenciaDto2In asistencia)
        {
            var nombreDeLaFoto = $"{Guid.NewGuid()}.jpeg";
            _almancenDeArchivos.ColocarCarpeta(asistencia.Ruta, contenedor);
            var rutaDeLaFoto = await _almancenDeArchivos.Guardar(nombreDeLaFoto, asistencia.Selfie);
            var entity = new AsistenciaEntity
            {
                UrlDeLaFoto = rutaDeLaFoto,
                Gps = asistencia.Gps,
                UsuarioId = asistencia.UsuarioId
            };
            var id = await _repositorio.AgregarAsync(entity);

            return id;
        }

        public async Task<List<AsistenciaDto>> ObtenerListaPorUsuarioIdAsync(int usuarioId)
        {
            var entities = await _repositorio.ObtenerListaPorUsuarioIdAsync(usuarioId);
            List<AsistenciaDto> lista = entities.Select(x => new AsistenciaDto
            {
                Gps = x.Gps,
                FechaDeRegistro = x.FechaDeRegistro,
                UrlSelfie = x.UrlDeLaFoto
            }).ToList();

            return lista;
        }
    }
   
}
