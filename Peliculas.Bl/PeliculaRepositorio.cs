using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Peliculas.Bl
{
    public class PeliculaRepositorio
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<Pelicula> _collection;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public PeliculaRepositorio(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<Pelicula>("Peliculas");
        }
        public async Task<int> AgregarAsync(Pelicula pelicula)
        {
            pelicula.Id = await ObtenerIdAsync();
            await _collection.InsertOneAsync(pelicula);

            return pelicula.Id;
        }

        public async Task ActualizarAsync(Pelicula pelicula) => await _collection.ReplaceOneAsync(x=> x.Id == pelicula.Id, pelicula);

        private async Task<int> ObtenerIdAsync()
        {
            var ultimoRegistro = await _collection
            .Find(FilterDefinition<Pelicula>.Empty)
            .SortByDescending(r => r.Id)
            .FirstOrDefaultAsync();
            if (ultimoRegistro == null)
                return 1;
            else
                return (ultimoRegistro.Id + 1);
        }

        internal async Task<List<Pelicula>> ObtenerAsync() => await _collection.Find(_ => true).ToListAsync();

        internal async Task<Pelicula> ObtenerAsync(int id) => await _collection.Find(x=> x.Id == id).FirstOrDefaultAsync();
    }
}
