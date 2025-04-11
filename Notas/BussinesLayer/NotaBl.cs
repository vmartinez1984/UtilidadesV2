using Notas.Dtos;
using Notas.Entities;
using Notas.Persistence;

namespace Notas.BussinesLayer
{
    public class NotaBl
    {
        private readonly NotaRepositorio _notaRepositorio;

        public NotaBl(NotaRepositorio notaRepositorio)
        {
            _notaRepositorio = notaRepositorio;
        }

        public async Task<List<NotaDto>> ObtenerAsync()
        {
            List<NotaDto> notas;

            notas = (await _notaRepositorio.ObtenerTodosAsync()).Select(x => new NotaDto
            {
                Contenido = x.Contenido,
                Estado = x.Estado,
                Id = Guid.Parse(x.Id),
                Nombre = x.Nombre,
                Tags = x.Tags
            }).ToList();

            return notas;
        }

        public async Task<string> AgregarAsync(NotaDto notaDto) => await _notaRepositorio.AgregarAsync(new NotaEntity
        {
            Contenido = notaDto.Contenido,
            Estado = notaDto.Estado,
            FechaDeRegistro = DateTime.Now,
            Id = notaDto.Id.ToString(),
            Nombre = notaDto.Nombre,
            Tags = notaDto.Tags,
            FechaFin = notaDto.FechaFin,
            FechaInicio = notaDto.FechaInicio,
        });

        public async Task Actaulizar(string id, NotaUpdateDto nota)
        {
            NotaEntity notaEntity;

            notaEntity = await _notaRepositorio.ObtenerPorIdAsync(id);           
            notaEntity.Nombre = nota.Nombre;
            notaEntity.Estado = nota.Estado;
            notaEntity.Tags = nota.Tags;
            notaEntity.Contenido = nota.Contenido;
            notaEntity.FechaFin = nota.FechaFin;
            notaEntity.FechaInicio = nota.FechaInicio;
            await _notaRepositorio.ActualizarAsync(notaEntity);

        }
    }

}
