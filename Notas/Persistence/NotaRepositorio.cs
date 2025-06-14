using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Notas.Entities;

namespace Notas.Persistence
{
    public class NotaRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private IMongoCollection<NotaEntity> _collection;

        public NotaRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<NotaEntity>("NotasV2");
        }

        public async Task<List<NotaEntity>> ObtenerTodosAsync(string carpeta)
        {
            List<NotaEntity> notas;
            if(string.IsNullOrEmpty(carpeta)) 
                notas = (await _collection.FindAsync(_ => true)).ToList();
                //notas = (await _collection.FindAsync(x => x.Carpeta == null)).ToList();
            else
                notas = (await _collection.FindAsync(x=> x.Carpeta == carpeta)).ToList();

            return notas;
        }

        public async Task<string> AgregarAsync(NotaEntity item)
        {
            await _collection.InsertOneAsync(item);

            return item._id.ToString();
        }

        public async Task<NotaEntity> ObtenerPorIdAsync(string id)
        {
            return (await _collection.FindAsync(x => x.EncodedKey == id)).FirstOrDefault();
        }

        public async Task ActualizarAsync(NotaEntity notaEntity)
        {
            await _collection.ReplaceOneAsync(x => x.EncodedKey == notaEntity.EncodedKey, notaEntity);
        }

        internal void AgregarColeccion(string v)
        {
            _collection = _mongoDatabase.GetCollection<NotaEntity>(v);
        }
    }
}
