using Microsoft.Extensions.Options;
using MongoDB.Driver;
using utilidadesv2.Entidades;

namespace utilidadesv2.Repositorio
{
    public class RepositorioDeNombresYApellidosMx
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ApellidoNombre> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSettings"></param>
        public RepositorioDeNombresYApellidosMx(IOptions<DataSettingsMongoDb> dataSettings)
        {
            var mongoClient = new MongoClient(dataSettings.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(dataSettings.Value.DatabaseName);

            _collection = _mongoDatabase.GetCollection<ApellidoNombre>(dataSettings.Value.CollectionName);
        }
        internal async Task AgregarApellidosAsync(List<ApellidoNombre> lista)
        {
           await _collection.InsertManyAsync(lista);
        }

        internal async Task AgregarNombresAsync(List<ApellidoNombre> lista)
        {
            await _collection.InsertManyAsync(lista);
        }

        internal async Task<List<ApellidoNombre>> ObtenerApellidosAsync()
        {
            List<ApellidoNombre> lista;

            lista = (await _collection.FindAsync(x=>x.Tipo == "Apellido")).ToList();

            return lista;
        }

        internal async Task<List<ApellidoNombre>> ObtenerNombresAsync()
        {
            List<ApellidoNombre> lista;

            lista = (await _collection.FindAsync(x=>x.Tipo == "Nombre Hombre")).ToList();

            return lista;
        }
    }
}