using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Utilidades.Repositorios.Entidades;

namespace Utilidades.Repositorios
{
    public class DireccionRepositorio
    {

        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Direccion> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSettings"></param>
        public DireccionRepositorio(IOptions<DataSettingsMongoDb> dataSettings)
        {
            var mongoClient = new MongoClient(dataSettings.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(dataSettings.Value.DatabaseName);

            _collection = _mongoDatabase.GetCollection<Direccion>("Direcciones");
        }

        /// <summary>
        /// Obtiene un codigo postal aleatorio por estado
        /// </summary>
        /// <param name="estado"></param>
        /// <returns></returns>
        public async Task<Direccion> ObtenerAleatorioAsync(string estado)
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
            var objeto = BsonSerializer.Deserialize<Direccion>(result);

            return objeto;
        }

        public async Task<Direccion> ObtenerAleatorioAsync()
        {          

            // Agregación para obtener un documento aleatorio
            var pipeline = new[]
            {
                new BsonDocument("$sample", new BsonDocument("size", 1)) // Obtiene 1 documento aleatorio
            };

            var result = await _collection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
            var data = await _collection.Find(x => x.Estado == "Aguascalientes").FirstOrDefaultAsync();
            var objeto = BsonSerializer.Deserialize<Direccion>(result);

            return objeto;

        }
    }
}
