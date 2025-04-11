using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Notas.Entities;

namespace Notas.Persistence
{
    public class EstadoRepositorio
    {
        private readonly IMongoCollection<EstadoEntity> _collection;
        public EstadoRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            var mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = mongoDatabase.GetCollection<EstadoEntity>("NotaEstados");
        }

        public async Task<List<EstadoEntity>> ObtenerTodosAsync()
        {
            List<EstadoEntity> notas;

            notas = (await _collection.FindAsync(_ => true)).ToList();

            return notas;
        }       
    }
}