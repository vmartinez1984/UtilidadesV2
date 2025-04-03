using Utilidades.Repositorios;
using Utilidades.Repositorios.Entidades;
using Utilidades.Servicios.Dtos;

namespace Utilidades.Servicios
{
    public class DireccionServicio
    {
        private readonly DireccionRepositorio _repositorio;

        public DireccionServicio(
            DireccionRepositorio direccionRepositorio
        )
        {
            _repositorio = direccionRepositorio;
        }

        public async Task<DireccionDto> OtenerAleatorio()
        {
            Direccion direccion;

            direccion = await _repositorio.ObtenerAleatorioAsync();

            return direccion.ToDto();
        }
    }

    public static class Mapeador
    {
        public static DireccionDto ToDto(this Direccion entidad)
            => entidad is null ? null : new DireccionDto
            {
                CalleYNumero = entidad.CalleYNumero,
                CodigoPostal = entidad.CodigoPostal,
                Colonia = entidad.Colonia,
                Estado = entidad.Estado,
                Municipio = entidad.Municipio,
                Nombre = entidad.Nombre,
                Telefono = entidad.Telefono,
                Gps = $"{entidad.Latitud},{entidad.Longitud}"
            };
    }
}
