using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Contador.BusinessLayer
{
    public class ContadorService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Contador> _collection;

        public ContadorService(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<Contador>("Contadores");
        }

        public async Task<Contador> Crear(ContadorDto dto)
        {
            var contador = new Contador
            {
                Key = dto.Key,
                ValorMin = dto.ValorMin,
                ValorMax = dto.ValorMax,
                ValorActual = dto.ValorMin
            };

            await _collection.InsertOneAsync(contador);

            return contador;
        }

        public async Task<int?> GetActual(string key)
        {
            var contador = await _collection.Find(x => x.Key == key)
                                            .FirstOrDefaultAsync();

            return contador?.ValorActual;
        }

        public async Task<int?> Next(string key)
        {
            var update = Builders<Contador>.Update.Inc(x => x.ValorActual, 1);

            var options = new FindOneAndUpdateOptions<Contador>
            {
                ReturnDocument = ReturnDocument.After
            };

            var contador = await _collection.FindOneAndUpdateAsync(
                x => x.Key == key,
                update,
                options
            );

            if (contador == null)
                return null;

            if (contador.ValorActual > contador.ValorMax)
                throw new Exception("Se alcanzó el valor máximo");

            return contador.ValorActual;
        }
    }
}
