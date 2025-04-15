using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Utilidades.Repositorios.Entidades;
using Microsoft.Extensions.Configuration;

namespace Utilidades.Repositorios
{
    public class RepositorioDeCodigoPostal
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<CodigoPostalEntity> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSettings"></param>
        public RepositorioDeCodigoPostal(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<CodigoPostalEntity>("CodigosPostales");
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
