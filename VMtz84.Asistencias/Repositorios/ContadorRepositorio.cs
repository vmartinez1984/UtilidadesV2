using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace VMtz84.Asistencias.Repositorios
{
    public class ContadorRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ContadorEntity> _collection;

        public ContadorRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<ContadorEntity>("Contador");
        }

        public int ObtenerSiguienteId(string nombreDeLaColeccion)
        {
            var filtro = Builders<ContadorEntity>.Filter
           .Eq(c => c.Id, nombreDeLaColeccion);

            var actualizacion = Builders<ContadorEntity>.Update
                .Inc(c => c.Secuencia, 1);

            var opciones = new FindOneAndUpdateOptions<ContadorEntity>
            {
                IsUpsert = true,
                ReturnDocument = ReturnDocument.After
            };

            var contador = _collection.FindOneAndUpdate(
                filtro,
                actualizacion,
                opciones
            );

            return contador.Secuencia;
        }
    }
}
