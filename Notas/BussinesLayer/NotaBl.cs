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

        public async Task<List<NotaDto>> ObtenerAsync(string carpeta = null)
        {
            List<NotaDto> notas;

            notas = (await _notaRepositorio.ObtenerTodosAsync(carpeta)).Select(x => new NotaDto
            {
                Tags = x.Tags,
                Valor01 = x.Valor01,
                Valor02 = x.Valor02,
                Valor03 = x.Valor03,
                Valor04 = x.Valor04,
                EncodedKey = x.EncodedKey
            }).ToList();

            return notas;
        }

        public async Task<string> AgregarAsync(NotaDto notaDto, string carpeta = null) => await _notaRepositorio.AgregarAsync(new NotaEntity
        {
            Tags = notaDto.Tags,
            EncodedKey = notaDto.EncodedKey,
            Carpeta = carpeta,
            Valor01 = notaDto.Valor01,
            Valor02 = notaDto.Valor02,
            Valor03 = notaDto.Valor03,
            Valor04 = notaDto.Valor04
        });

        public async Task Actaulizar(string id, NotaDtoIn nota)
        {
            NotaEntity notaEntity;

            notaEntity = await _notaRepositorio.ObtenerPorIdAsync(id);        
            notaEntity.Tags = nota.Tags;
            notaEntity.Valor01 = nota.Valor01;
            notaEntity.Valor02 = nota.Valor02;
            notaEntity.Valor03 = nota.Valor03;
            notaEntity.Valor04 = nota.Valor04;

            await _notaRepositorio.ActualizarAsync(notaEntity);
        }

        public void AgregarColeccion(string v)
        {
            _notaRepositorio.AgregarColeccion(v);
        }
    }

}
