using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using VMtz84.Pizzas.Dtos;
using VMtz84.Pizzas.Entities;

namespace VMtz84.Pizzas.Services
{
    public class ClienteService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<UsuarioEntity> _collection;

        public ClienteService(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<UsuarioEntity>("Usuarios");
        }

        public async Task<string> AgregarAsync(ClienteDto usuario)
        {
            UsuarioEntity entity;

            entity = new UsuarioEntity
            {
                Id = await ObtenerIdAsync(),
                Encodedkey = Guid.NewGuid().ToString(), // Genera una clave única
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                Contrasenia = usuario.Contrasenia,
                FechaDeRegistro = DateTime.UtcNow,
                EstaActivo = true
            };
            await _collection.InsertOneAsync(entity);

            return usuario.Encodedkey;
        }

        private async Task<int> ObtenerIdAsync()
        {
            var lastEntity = await _collection.Find(_ => true).SortByDescending(e => e.Id).FirstOrDefaultAsync();
            return lastEntity != null ? lastEntity.Id + 1 : 1;
        }

        public async Task<ClienteDto> ObtenerPorCorreoAsync(string correo)
        {
            UsuarioEntity entity;

            entity = await _collection.Find(e => e.Correo == correo).FirstOrDefaultAsync();

            return new ClienteDto
            {
                Apellidos = entity.Apellidos,
                Correo = entity.Correo,
                Encodedkey = entity.Encodedkey,
                Nombre = entity.Nombre
            };
        }

        public async Task<bool> ValidarCredencialesAsync(string correo, string contraseña)
        {
            UsuarioEntity entity;

            entity = await _collection.Find(e => e.Correo == correo).FirstOrDefaultAsync();

            return entity != null && entity.Contrasenia == contraseña;
        }
    }

    public class DireccionService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<DireccionEntity> _collection;
        public DireccionService(IConfiguration configuration)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<DireccionEntity>("Direcciones");
        }
        public async Task<string> AgregarAsync(DireccionDto direccion)
        {
            DireccionEntity entity;
            entity = new DireccionEntity
            {
                Id = await ObtenerIdAsync(),
                Encodedkey = Guid.NewGuid().ToString(), // Genera una clave única
                UsuarioEncodedkey = direccion.UsuarioEncodedkey,
                Calle = direccion.Calle,                
                Colonia = direccion.Colonia,
                CodigoPostal = direccion.CodigoPostal,
                Ciudad = direccion.Ciudad,
                Estado = direccion.Estado                
            };
            await _collection.InsertOneAsync(entity);
            return entity.Encodedkey;
        }

        private async Task<int> ObtenerIdAsync()
        {
            var lastEntity = await _collection.Find(_ => true).SortByDescending(e => e.Id).FirstOrDefaultAsync();
            return lastEntity != null ? lastEntity.Id + 1 : 1;
        }

        public async Task<List<DireccionDto>> ObtenerPorIdAsync(string usuarioEncodedkey)
        {
            List<DireccionEntity> entities;
            entities = await _collection.Find(e => e.Encodedkey == usuarioEncodedkey).ToListAsync();
            return entities.Select(x => new DireccionDto
            {
                Calle = x.Calle,
                Ciudad = x.Ciudad,
                Colonia = x.Colonia,
                CodigoPostal = x.CodigoPostal,
                Estado = x.Estado,
                EsLaPrincipal = x.EsLaPrincipal,
                UsuarioEncodedkey = x.UsuarioEncodedkey
            }).ToList();
        }
    }
}