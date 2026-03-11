using Microsoft.Extensions.Configuration;
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

        public async Task<List<ProductoEntity>> ObtenerTodosAsync(string llave, bool estaActivo)
        {
            List<ProductoEntity> notas;

            notas = (await _collection.FindAsync(x => x.Llave == llave && x.EstaActivo == estaActivo)).ToList();

            return notas;
        }

        public async Task<string> AgregarAsync(ProductoEntity item)
        {
            await _collection.InsertOneAsync(item);

            return item._id.ToString();
        }

        public async Task<ProductoEntity> ObtenerPorIdAsync(string idEncodedKJey)
        {
            var entity = await _collection.Find(x => x.EncodedKey == idEncodedKJey).FirstOrDefaultAsync();
            if (entity is not null)
                return entity;
            return await _collection.Find(x => x._id == idEncodedKJey).FirstOrDefaultAsync();
        }

        public async Task ActualizarAsync(ProductoEntity entity)
        {
            await _collection.ReplaceOneAsync(x => x.EncodedKey == entity.EncodedKey, entity);
        }
    }
}
