


namespace ProductoBusinessLayer
{
    public class ProductoBl
    {
        private readonly ProductoRepositorio _repositorio;

        public ProductoBl(ProductoRepositorio notaRepositorio)
        {
            _repositorio = notaRepositorio;
        }

        public async Task<List<ProductoDto>> ObtenerAsync(string llave)
        {
            List<ProductoDto> lista;

            lista = (await _repositorio.ObtenerTodosAsync(llave)).Select(x => new ProductoDto
            {                
                Valor01 = x.Valor01,
                Valor02 = x.Valor02,
                Valor03 = x.Valor03,
                Valor04 = x.Valor04,
                EncodedKey = x.EncodedKey,
                EstaActivo = x.EstaActivo,
                FechaDeRegistro = x.FechaDeRegistro,
            }).ToList();

            return lista;
        }

        public async Task<string> AgregarAsync(ProductoDto notaDto, string carpeta = null) => await _repositorio.AgregarAsync(new ProductoEntity
        {            
            EncodedKey = notaDto.EncodedKey,            
            Valor01 = notaDto.Valor01,
            Valor02 = notaDto.Valor02,
            Valor03 = notaDto.Valor03,
            Valor04 = notaDto.Valor04
        });

        public async Task ActualizarAsync(string id, ProductoDto nota)
        {
            ProductoEntity notaEntity;

            notaEntity = await _repositorio.ObtenerPorIdAsync(id);            
            notaEntity.Valor01 = nota.Valor01;
            notaEntity.Valor02 = nota.Valor02;
            notaEntity.Valor03 = nota.Valor03;
            notaEntity.Valor04 = nota.Valor04;

            await _repositorio.ActualizarAsync(notaEntity);
        }

        public async Task<ProductoDto> ObtenerPorIdAsync(string idEncodedKey)
        {
            ProductoEntity entity;

            entity = await _repositorio.ObtenerPorIdAsync(idEncodedKey);

            return new ProductoDto
            {
                EncodedKey = entity.EncodedKey,
                EstaActivo = entity.EstaActivo,
                FechaDeRegistro = entity.FechaDeRegistro,
                Valor01 = entity.Valor01,
                Valor02 = entity.Valor02,
                Valor03 = entity.Valor03,
                Valor04 = entity.Valor04
            };
        }
               
        public async Task ActivarAsync(string idEncodedKey, bool estaActivo)
        {
            ProductoEntity entity;

            entity = await _repositorio.ObtenerPorIdAsync(idEncodedKey);
            entity.EstaActivo = estaActivo;
            await _repositorio.ActualizarAsync(entity);
        }
    }
}
