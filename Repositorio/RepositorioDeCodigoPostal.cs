using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
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
            BsonDocument filtro;

            // Definir el filtro
            if (int.TryParse(estado, out int estadoId))
            {                
                filtro = new BsonDocument("EstadoId", estadoId); // Reemplaza con tu condición
            }
            else
            {
                filtro = new BsonDocument("Estado", estado); // Reemplaza con tu condición             
            }

            // Agregación para obtener un documento aleatorio
            var pipeline = new[]
            {
                new BsonDocument("$match", filtro),  // Filtrar documentos
                new BsonDocument("$sample", new BsonDocument("size", 1)) // Obtiene 1 documento aleatorio
            };

            var result = await _collection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
            var objeto = BsonSerializer.Deserialize<CodigoPostalEntity>(result);           

            return objeto;
        }
    }
}
