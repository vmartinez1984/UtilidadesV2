using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using VMtz84.Asistencias.Entities;

namespace VMtz84.Asistencias.Repositorios
{
    public class AsistenciaRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<AsistenciaEntity> _collection;
        private readonly ContadorRepositorio _contador;
        private const string NombreDeLaColeccion = "Asistencias";
        public AsistenciaRepositorio(IConfiguration configuration, ContadorRepositorio contadorRepositorio)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<AsistenciaEntity>(NombreDeLaColeccion);
            this._contador = contadorRepositorio;
        }

        public async Task<int> AgregarAsync(AsistenciaEntity entity)
        {
            entity.Id = _contador.ObtenerSiguienteId(NombreDeLaColeccion);
            await _collection.InsertOneAsync(entity);

            return entity.Id;
        }

        public async Task<List<AsistenciaEntity>> ObtenerListaPorUsuarioIdAsync(int id)
        {
            List<AsistenciaEntity> lista;

            lista = await _collection.Find(x => x.UsuarioId == id).ToListAsync();

            return lista;
        }
    }
}
