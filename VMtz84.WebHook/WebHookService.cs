using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace VMtz84.WebHook
{
    public class WebHookService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<WebHook> _collection;

        public WebHookService(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<WebHook>("WebHooks");
        }

        public async Task GuardarWebHook(WebHook webHook)
        {
            await _collection.InsertOneAsync(webHook);
        }

        public async Task<List<WebHook>> ObtenerWebHooks()
        {
            return (await _collection.Find(_ => true).ToListAsync()).OrderByDescending(x=> x.FechaDeRegistro).ToList();
        }
    }
}
