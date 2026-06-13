using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using VMtz84.Pizzas.Dtos;
using VMtz84.Pizzas.Entities;

namespace VMtz84.Pizzas.Services
{
    public class OrdenService
    {
        private readonly IMongoDatabase _mongoDatabase;
        private readonly IMongoCollection<OrdenEntity> _collection;
        private readonly MenuService _pizzaService;

        public OrdenService(IConfiguration configuration, MenuService pizzaService)
        {
            string stringConnection = configuration.GetConnectionString("Utilidades");
            var mongoClient = new MongoClient(stringConnection);
            string databaseName = stringConnection.Split("/").Last().Split("?").First();
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
            _collection = _mongoDatabase.GetCollection<OrdenEntity>("Ordenes");
            _pizzaService = pizzaService;
        }

        public async Task<OrdenDto> ObtenerOrdenPorCliente(string clienteEncodedkey)
        {
            var filter = Builders<OrdenEntity>.Filter.Eq(e => e.ClienteEncodedkey, clienteEncodedkey);
            var sort = Builders<OrdenEntity>.Sort.Descending(e => e.FechaDeRegistro);

            var entity = await _collection.Find(filter)
                                          .Sort(sort)
                                          .Limit(1)
                                          .FirstOrDefaultAsync();

            OrdenDto ordenDto = new OrdenDto
            {
                Estado = entity.Estado,
                MetodoDePago = entity.MetodoDePago,
                Pizzas = entity.Pizzas.Select(pizza => new PizzaDto
                {
                    PizzaId = pizza.PizzaId,
                    Pizza2Id = pizza.Pizza2Id,
                    Nombre1 = pizza.Nombre1, // Asumiendo que el nombre de la pizza es su ID
                    Nombre2 = pizza.Nombre2, // Asumiendo que el nombre de la pizza es su ID
                    Masa = pizza.Masa, // Asumiendo que el nombre de la masa es su ID
                    Tamanio = pizza.Tamanio,
                    Precio = pizza.Precio
                }).ToList(),
                Productos = entity.Productos.Select(producto => new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Ingredientes = producto.Ingredientes,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Ruta = producto.Ruta,
                    Menu = producto.Menu
                }).ToList()
            };

            return ordenDto;
        }

        public async Task<string> AgregarAsync(OrdenDtoIn ordenDto)
        {
            OrdenEntity entity;

            entity = new OrdenEntity
            {
                Encodedkey = ordenDto.Encodedkey,
                FechaDeRegistro = DateTime.Now,
                ClienteEncodedkey = ordenDto.ClienteEncodedkey,
                Pizzas = ObtenerPizzas(ordenDto.Pizzas),
                Productos = ordenDto.Productos.Select(producto => new ProductoEntity
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Ingredientes = producto.Ingredientes,
                    Precio = producto.Precio,
                    Descripcion = producto.Descripcion,
                    Ruta = producto.Ruta,
                    Menu = producto.Menu,
                }).ToList(),
                Estado = "En preparación",
                MetodoDePago = ordenDto.MetodoDePago
            };

            await _collection.InsertOneAsync(entity);

            return entity.Encodedkey;
        }

        private List<PizzaEntity> ObtenerPizzas(List<PizzaDtoIn> pizzas)
        {
            List<PizzaEntity> entities = new List<PizzaEntity>();

            pizzas.ForEach(pizza =>
            {
                var productoDto = _pizzaService.ObtenerPizza(pizza.PizzaId);
                var producto2Dto = _pizzaService.ObtenerPizza(pizza.Pizza2Id);
                var masaDto = _pizzaService.ObtenerMasa(pizza.MasaId);
                var pizzaEntity = new PizzaEntity
                {
                    PizzaId = pizza.PizzaId,
                    Nombre1 = productoDto.Nombre,
                    Pizza2Id = pizza.Pizza2Id,
                    Nombre2 = producto2Dto is null ? string.Empty : producto2Dto.Nombre,
                    Masa = masaDto.Nombre,
                    Tamanio = pizza.Tamanio,
                    Precio = productoDto.Precio
                };
                entities.Add(pizzaEntity);
            });

            return entities;
        }
    }
}
