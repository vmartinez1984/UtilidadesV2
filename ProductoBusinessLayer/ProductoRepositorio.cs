using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ProductoBusinessLayer
{
    public class ProductoRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ProductoEntity> _collection;

        public ProductoRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<ProductoEntity>("Productos");
        }

        public async Task<List<ProductoEntity>> ObtenerTodosAsync(string llave)
        {
            List<ProductoEntity> notas;

            notas = (await _collection.FindAsync(x => x.Llave == llave)).ToList();

            return notas;
        }

        private async Task<int> ObtenerId()
        {
            var item = await
            _collection
            .Find(new BsonDocument()) // Puedes agregar filtros si es necesario
            .SortByDescending(r => r.Id) // Ordenar por fecha de forma descendente
            .FirstOrDefaultAsync();
            ;
            if (item == null)
                return 1;

            return item.Id + 1;
        }

        public async Task<string> AgregarAsync(ProductoEntity item)
        {
            item.Id = await ObtenerId();
            await _collection.InsertOneAsync(item);

            return item._id.ToString();
        }

        public async Task<ProductoEntity> ObtenerPorIdAsync(string idEncodedKJey)
        {
            if (int.TryParse(idEncodedKJey, out int id))
                return (await _collection.FindAsync(x => x.Id == id)).FirstOrDefault();
            else
                return (await _collection.FindAsync(x => x.EncodedKey == idEncodedKJey)).FirstOrDefault();
        }

        public async Task ActualizarAsync(ProductoEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.EncodedKey == entity.EncodedKey, entity);
        }        
    }
}
