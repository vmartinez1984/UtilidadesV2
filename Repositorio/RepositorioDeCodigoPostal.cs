using Microsoft.Extensions.Options;
using MongoDB.Driver;
using utilidadesv2.Entidades;

namespace utilidadesv2.Repositorio
{
    public class RepositorioDeCodigoPostal
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<CodigoPostalEntity> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSettings"></param>
        public RepositorioDeCodigoPostal(IOptions<DataSettingMongoDbCodigosPostales> dataSettings)
        {
            var mongoClient = new MongoClient(dataSettings.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(dataSettings.Value.DatabaseName);

            _collection = _mongoDatabase.GetCollection<CodigoPostalEntity>(dataSettings.Value.CollectionName);
        }

        /// <summary>
        /// Obtiene un codigo postal aleatorio por estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public async Task<CodigoPostalEntity> ObtenerCodigoPostalAleatorioAsync(string estado)
        {
            var random = new Random();
            FilterDefinition<CodigoPostalEntity> filter;

            if (int.TryParse(estado, out int estadoId))
            {
                filter = Builders<CodigoPostalEntity>.Filter.Eq(x => x.EstadoId, estadoId);
            }
            else
            {
                filter = Builders<CodigoPostalEntity>.Filter.Eq(x => x.Estado, estado);
            }

            // Obtén el número total de documentos que cumplen el filtro
            var totalDocuments = await _collection.CountDocumentsAsync(filter);

            if (totalDocuments == 0)
            {
                return null; // O maneja el caso en que no se encuentren documentos
            }

            // Selecciona un índice aleatorio
            var randomIndex = random.Next(0, (int)totalDocuments);

            // Usa Find para obtener solo el documento necesario
            var resultado = await _collection.Find(filter)
                                             .Skip(randomIndex)
                                             .Limit(1)
                                             .FirstOrDefaultAsync();

            return resultado;
        }
    }
}
