using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Utilidades.Repositorios.Entidades;

namespace Utilidades.Repositorios
{
    public class NombreRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<ApellidoNombre> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSettings"></param>
        public NombreRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<ApellidoNombre>("NombreApellidosMx");
        }
        internal async Task AgregarAsync(List<ApellidoNombre> lista)
        {
            await _collection.InsertManyAsync(lista);
        }

        internal async Task BorrarColeccionAsync()
        {
            await _mongoDatabase.DropCollectionAsync(_collection.CollectionNamespace.CollectionName);
        }

        public async Task<string> ObtenerApellidoAleatorioAsync()
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

        /// <summary>
        /// genero 1 hombre, 0 mujer
        /// </summary>
        /// <param name="genero"></param>
        /// <returns></returns>
        public async Task<string> ObtenerNombresAsync(int genero)
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

        internal async Task AgregarApellidosAsync(List<ApellidoNombre> lista)
        {
            int id;
            id = (int)await _collection.EstimatedDocumentCountAsync() + 1;
            foreach (var item in lista)
            {
                item.Id = id;
                id++;
            }
            await _collection.InsertManyAsync(lista);
        }



        internal async Task AgregarNombresAsync(List<ApellidoNombre> lista)
        {
            int id;
            id = (int)await _collection.EstimatedDocumentCountAsync() + 1;
            foreach (var item in lista)
            {
                item.Id = id;
                id++;
            }
            await _collection.InsertManyAsync(lista);
        }

        internal async Task<List<ApellidoNombre>> ObtenerApellidosAsync()
        {
            List<ApellidoNombre> lista;

            lista = (await _collection.FindAsync(x => x.Tipo == "Apellido")).ToList();

            return lista;
        }

        internal async Task<List<ApellidoNombre>> ObtenerNombresAsync()
        {
            List<ApellidoNombre> lista;

            lista = (await _collection.FindAsync(x => x.Tipo == "Nombre Hombre")).ToList();

            return lista;
        }
    }
}
