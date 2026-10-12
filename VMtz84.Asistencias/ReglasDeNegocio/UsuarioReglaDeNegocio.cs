using VMtz84.Asistencias.Dtos;
using VMtz84.Asistencias.Entities;
using VMtz84.Asistencias.Repositorios;

namespace VMtz84.Asistencias.ReglasDeNegocio
{
    public class UsuarioReglaDeNegocio
    {
        private readonly UsuarioRepositorio usuarioRepositorio;

        public UsuarioReglaDeNegocio(UsuarioRepositorio usuarioRepositorio)
        {
            this.usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<UsuarioDto> ObtenerAsync(InicioDeSesionDto inicioDeSesionDto)
        {
            var entity = await usuarioRepositorio.ObtenerPorCorreoAsync(inicioDeSesionDto.Correo);
            if (entity == null)
                return null;
            if (entity.Contrasenia != inicioDeSesionDto.Contrasenia)
                return null;

            return new UsuarioDto
            {
                Correo = entity.Correo,
                Nombre = entity.Nombre,
                Apellidos = entity.Apellidos,
                Id = entity.Id
            };
        }

        public async Task<bool> ExiteCorreoRegistradoPreviamente(string correo)
        {
            var entity = await usuarioRepositorio.ObtenerPorCorreoAsync(correo);
            if (entity == null)
                return false;

            return true;
        }

        public async Task<int> AgregarAsync(UsuarioDtoIn usuario)
        {
            var entity = new UsuarioEntity
            {
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Contrasenia = usuario.Contrasenia,
                Correo = usuario.Correo
            };
            var id = await usuarioRepositorio.AgregarAsync(entity);

            return id;
        }
    }
}
