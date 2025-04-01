using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using utilidadesv2.Entidades;

namespace utilidadesv2.Repositorio
{
    public class RepositorioDeNombresYApellidosEs
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ApellidoNombre> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSettings"></param>
        public RepositorioDeNombresYApellidosEs(IOptions<DataSettingsMongoDb> dataSettings)
        {
            var mongoClient = new MongoClient(dataSettings.Value.ConnectionString);

            _mongoDatabase = mongoClient.GetDatabase(dataSettings.Value.DatabaseName);

            _collection = _mongoDatabase.GetCollection<ApellidoNombre>(dataSettings.Value.CollectionName2);
        }
        internal async Task AgregarAsync(List<ApellidoNombre> lista)
        {
            await _collection.InsertManyAsync(lista);
        }

        internal async Task BorrarColeccionAsync()
        {
            await _mongoDatabase.DropCollectionAsync(_collection.CollectionNamespace.CollectionName);
        }

        internal async Task<string> ObtenerApellidoAleatorioAsync()
        {
            // Definir el filtro
            var filtro = new BsonDocument("Tipo", "Apellido"); // Reemplaza con tu condición
            // Agregación para obtener un documento aleatorio
            var pipeline = new[]
            {
                new BsonDocument("$match", filtro),  // Filtrar documentos
                new BsonDocument("$sample", new BsonDocument("size", 1)) // Obtiene 1 documento aleatorio
            };

            var result = await _collection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
            var objeto = BsonSerializer.Deserialize<ApellidoNombre>(result);

            return objeto.Dato;
        }

        internal async Task<List<ApellidoNombre>> ObtenerApellidosAsync()
        {
            List<ApellidoNombre> lista;

            lista = (await _collection.FindAsync(x => x.Tipo == "Apellido")).ToList();

            return lista;
        }

        /// <summary>
        /// genero 1 hombre, 0 mujer
        /// </summary>
        /// <param name="genero"></param>
        /// <returns></returns>
        internal async Task<string> ObtenerNombresAsync(int genero)
        {            
            Random random = new Random();
            BsonDocument filtro;

            if (genero == 1)
                filtro = new BsonDocument("Tipo", "Nombre Hombre");
            else
                filtro = new BsonDocument("Tipo", "Nombre Mujer");

            var pipeline = new[]
            {
                new BsonDocument("$match", filtro),  // Filtrar documentos
                new BsonDocument("$sample", new BsonDocument("size", 1)) // Obtiene 1 documento aleatorio
            };

            var result = await _collection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();
            var objeto = BsonSerializer.Deserialize<ApellidoNombre>(result);

            return objeto.Dato;
        }
    }
}