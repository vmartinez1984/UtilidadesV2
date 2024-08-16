using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
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
            List<ApellidoNombre> lista;
            Random random = new Random();

            lista = await ObtenerApellidosAsync();

            return lista[random.Next(0,lista.Count)].Dato;
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
            List<ApellidoNombre> lista;
            Random random = new Random();

            if (genero == 1)
                lista = (await _collection.FindAsync(x => x.Tipo == "Nombre Hombre")).ToList();
            else
                lista = (await _collection.FindAsync(x => x.Tipo == "Nombre Mujer")).ToList();
            lista = await ObtenerApellidosAsync();

            return lista[random.Next(0, lista.Count)].Dato;
        }
    }
}