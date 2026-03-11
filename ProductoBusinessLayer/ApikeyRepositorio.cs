using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace ProductoBusinessLayer
{
    public class ApikeyRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ApikeyEntity> _collection;

        public ApikeyRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<ApikeyEntity>("Apikeys");
        }

        public async Task<string> AgregarAsync(ApikeyEntity item)
        {
            await _collection.InsertOneAsync(item);

            return item._id.ToString();
        }                

        internal async Task<bool> ExisteAsync(string idEncodedKey)
        {
            var total = await _collection.CountDocumentsAsync(x => x.Apikey == idEncodedKey);
            return total == 1;
        }
    }

}