using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using VMtz84.Asistencias.Entities;

namespace VMtz84.Asistencias.Repositorios
{
    public class UsuarioRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<UsuarioEntity> _collection;
        private readonly ContadorRepositorio _contador;
        private const string NombreDeLaColeccion = "Usuarios";
        public UsuarioRepositorio(IConfiguration configuration, ContadorRepositorio contadorRepositorio)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<UsuarioEntity>(NombreDeLaColeccion);
            this._contador = contadorRepositorio;
        }

        public async Task<int> AgregarAsync(UsuarioEntity entity)
        {
            entity.Id = _contador.ObtenerSiguienteId(NombreDeLaColeccion);
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task<UsuarioEntity> ObtenerPorCorreoAsync(string correo)
        {
            UsuarioEntity entity;

            entity = await _collection.Find(e => e.Correo == correo).FirstOrDefaultAsync();

            return entity;
        }
    }
}
