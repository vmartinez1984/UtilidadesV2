using VMtz84.Pizzas.Dtos;

namespace VMtz84.Pizzas.Services
{
    public class PizzaService
    {
        public ClienteService Cliente { get; set; }

        public PizzaService(ClienteService clienteService)
        {
            Cliente = clienteService;
        }

        public List<MenuDto> ObtenerMenus()
        {
            var menus = new List<MenuDto>
            {
                new MenuDto
                {
                    Id = 1,
                    Titulo = "pizzas",
                    Subtitulo = "Escoge tu especialidad preferida",
                    Ruta = "/images/menus/pizza.png"
                },
                new MenuDto
                {
                    Id = 2,
                    Titulo = "pollo",
                    Subtitulo = "Jugosas y deliciosas opciones de pollo en una variedad de sabores",
                    Ruta = "/images/menus/chicken.png"
                },
                new MenuDto
                {
                    Id = 3,
                    Titulo = "adicionesles",
                    Subtitulo = "Complementa tu comida con tus adicionales favoritos",
                    Ruta = "/images/menus/breads.png"
                },
                new MenuDto
                {
                    Id = 4,
                    Titulo = "bebidas",
                    Subtitulo = "Para saciar tu sed",
                    Ruta = "/images/menus/drinks.png"
                },
                new MenuDto
                {
                    Id = 5,
                    Titulo = "postres",
                    Subtitulo = "Para satisfacer tus antojos",
                    Ruta = "/images/menus/desert.png"
                }
            };

            return menus;
        }

        public List<ProductoDto> ObtenerPizzas()
        {
            var pizzas = new List<ProductoDto>
            {
            new ProductoDto
            {
                Id = 1,
                Nombre = "Carbonara",
                Descripcion = "Queso crema, mozzarella, champiñones frescos, tocino y cebolla: la combinación perfecta para una pizza tan cremosa como irresistible",
                Ruta = "/images/pizzas/022238e3-ac62-45af-b765-379a077a9b26.jpg",
                Menu = "Pizza",
            },
            new ProductoDto
            {
                Id = 2,
                Nombre = "Triple peperoni",
                Descripcion = "Disfruta tres veces mas de tu ingrediente favorito en esta especialidad",
                Menu = "Pizza",
                Ruta = "/images/pizzas/00455854-40f9-45e2-a241-b03e70dfe6cc.jpg"
            },
            new ProductoDto
            {
                Id = 3,
                Nombre = "Peperoni",
                Descripcion = "Tu ingrediente favorito en una masa fresca y hecha a mano. (290 Cal)",
                Menu = "Pizza",
                Ruta = "/images/pizzas/be242cad-08db-4b00-becd-be48a9fb86a4.jpg"
            },
            new ProductoDto
            {
                Id = 4,
                Nombre = "Hawaiina",
                Descripcion = "La pizza que unos cuestionan pero todos aman. Jamón, piña. (290 Cal)",
                Menu = "Pizza",
                Ruta = "/images/pizzas/75b89c2b-2c59-4225-8478-4e9ab4432ec9.jpg"
            },
            new ProductoDto
            {
                Id = 5,
                Nombre = "Mexicana",
                Descripcion = "La pizza con los sabores auténticos de nuestro país. Chorizo, carne molida, jalapeño, cebolla. (310 Cal)",
                Menu = "Pizza",
                Ruta = "/images/pizzas/7f339d0c-bf13-4839-a9dc-de6721210a5b.jpg"
            },
            new ProductoDto
            {
                Id = 6,
                Nombre = "Texas bbq",
                Descripcion = "¡Ajúaa! Arre por esta pizza con salsa BBQ, queso mozzarella, tocino, pollo fresco y carne molida bien cocida, sabores que te harán amarla. (320 Cal)",
                Ingredientes = "Salsa BBQ, Salsa de tomate, Carne molida, Pollo, Tocino",
                Menu = "Pizza",
                Ruta = "/images/pizzas/67734d82-5c38-40f8-a311-255c0d157bd5.jpg"
            },

            new ProductoDto { Id = 7, Nombre = "Cuatro quesos", Ruta = "images/pizzas/0ad30a1e-d7ed-4384-9b1e-b8a5c9587174.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 8, Nombre = "Pepperoni especial", Ruta = "images/pizzas/8be27c50-bb3c-4774-ac8f-a41f42029617.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 9, Nombre = "Carnes frias", Ruta = "images/pizzas/f8748c06-e178-4d38-b99c-a522ad450a86.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 10, Nombre = "Chicken hawaiina", Ruta = "images/pizzas/49249843-d9af-4356-bcac-ae94e3357742.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 11, Nombre = "Honolulu", Ruta = "images/pizzas/cdcef20a-407a-488e-9751-6f481a871048.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 12, Nombre = "Veggie", Ruta = "images/pizzas/3f362b55-3f56-47f9-b67a-ff49eb10f66a.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 13, Nombre = "Extravaganzza", Ruta = "images/pizzas/db99ef86-f2b8-4a8e-8afb-5ad4409e4233.jpg", Menu = "Pizza" },
            new ProductoDto { Id = 14, Nombre = "Deluxe", Ruta = "images/pizzas/9614b6f3-0def-4ace-8c5d-e0954d899a5b.jpg", Menu = "Pizza" },
            };

            return pizzas;
        }

        public List<ProductoDto> ObtenerPollos()
        {
            var productos = new List<ProductoDto> {
                 new ProductoDto {
                     Id = 21,
                     Nombre =  "Alitas cayene",
                     Descripcion = "Salsa picosita con un toque de diferentes chiles (490 Cal)",
                     Precio = 139,
                     Ruta = "images/pollos/1ecf11f9-4e5d-460a-bead-521c53502a30.jpg",
                     Menu = "pollos"
                 },
                new ProductoDto
                {
                    Id = 22,
                    Nombre =  "Alitas BBQ",
                    Descripcion = "Con el sabor tradicional dulce ahumado que ya canoces (490 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/48365980-9741-410e-87db-57b760d6c941.jpg",
                    Menu = "pollos"
                },
                new ProductoDto {
                    Id = 23,
                    Nombre =  "Alitas Mango habanero",
                    Descripcion = "Una mezcla picosita de sabores dulce con habanero (490 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/fa57713d-549f-45c8-9d5c-9ba97f103c69.jpg",
                    Menu = "pollos"
                  },
                  new ProductoDto {
                    Id = 24,
                    Nombre =  "Alitas Naturales",
                    Descripcion = "Deliciosas y horneadas con gran sabor al natural (490 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/61b65a6c-ae9c-44a1-8f9e-b54a0a83ffd2.jpg",
                    Menu = "pollos"
                  },
                  new ProductoDto {
                    Id = 25,
                    Nombre =  "Boneless Cayenne",
                    Descripcion = "Salsa picosita con un toque de diferentes chiles (360 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/1ecf11f9-4e5d-460a-bead-521c53502a30.jpg",
                    Menu = "pollos"
                  },
                  new ProductoDto{
                    Id = 26,
                    Nombre =  "Boneless BBQ",
                    Descripcion = "Con el sabor tradicional dulce ahumado que ya canoces (390 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/0287fd68-2857-418e-b672-64a253107531.jpg",
                    Menu = "pollos"
                  },
                  new ProductoDto {
                    Id = 27,
                    Nombre =  "Boneless Mango habanero",
                    Descripcion = "Una mezcla picosita de sabores dulce con habanero (390 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/09959116-b5ef-4bbf-802f-85c57025226e.jpg",
                    Menu = "pollos"
                  },
                  new ProductoDto{
                    Id = 28,
                    Nombre =  "Boneless Naturales",
                    Descripcion = "Deliciosas y horneadas con gran sabor al natural (490 Cal)",
                    Precio = 139,
                    Ruta = "images/pollos/fafc0900-9fb2-4c12-9a85-f15830e0bfd0.jpg",
                    Menu = "pollos"
                  }
            };

            return productos;
        }

        public List<ProductoDto> ObtenerAdicionales()
        {
            var productos = new List<ProductoDto> {
                  new ProductoDto{
                    Id = 31,
                    Nombre = "Papotas",
                    Descripcion = "Gajos de papa horneada, con un toque picosito(250 Cal)",
                    Precio = 79,
                    Ruta = "images/adicionale/f1dfde0b-b388-4b7f-9a56-f9f1df41d11b.jpg",
                    Menu = "adicionales"
                  },
                  new ProductoDto {
                    Id = 32,
                    Nombre = "Cheesy Bread",
                    Descripcion = "Delicioso pan horneado relleno de queso crema y mozzarella gratinado con queso mozzarella, cheddar y parmesano",
                    Precio = 99,
                    Ruta = "images/adicionale/893340b6-16ab-4d86-90a3-6ea90d31edb6.jpg",
                    Menu = "adicionales"
                  },
                  new ProductoDto {
                    Id = 33,
                    Nombre = "Cheesy Bread chorizo jalapeño",
                    Descripcion = "Delicioso pan horneado al momento, relleno de queso crema, queso mozzarella, chorizo y jalapeño con un toque de finas hierbas. Gratinado con una capa de queso mozzarella, cheddar y parmesano",
                    Precio = 99,
                    Ruta = "images/adicionale/7decf559-592d-4891-b655-95284358b2c5.jpg",
                    Menu = "adicionales"
                  },
                  new ProductoDto{
                    Id = 34,
                    Nombre = "Mango Habanero",
                    Descripcion = "Gajos de papa horneada, con un toque picosito(250 Cal)",
                    Precio = 79,
                    Ruta = "images/adicionale/12a5d87c-d067-47d1-a445-db77a2fbf557.jpg",
                    Menu = "adicionales"
                  },
                  new ProductoDto{
                        Id = 35,
                        Nombre = "Cheespeño",
                        Descripcion = "Dip sabor queso con jalapeño (213 kcal)",
                        Precio = 20,
                        Ruta = "images/adicionale/32996a18-c4d4-4e54-bc94-03b4db402038.jpg",
                        Menu = "adicionales"
                  },
                  new ProductoDto {
                    Id = 36,
                    Nombre = "Brava",
                    Descripcion = "Salsa picante (21.8 kcal)",
                    Precio = 20,
                    Ruta = "images/adicionale/10716b71-e4b6-4875-831f-57d934290e51.jpg",
                    Menu = "adicionales"
                  },
                  new ProductoDto {
                    Id = 37,
                    Nombre = "BBQ",
                    Descripcion = "Salsa de chiles con especias (31.4 kcal) ",
                    Precio = 20,
                    Ruta = "images/adicionale/5406d44a-5607-44b0-876d-7bb931d3a28d.jpg",
                    Menu = "adicionales"
                  },
                  new ProductoDto
                  {
                      Id = 38,
                      Nombre = "Ranch",
                      Descripcion = "Aderezo sabor queso parmesano y ajo (203.3 kcal)",
                      Precio = 20,
                      Ruta = "images/adicionales/9c5ab2c7-14cb-4cdd-8e6a-7b836ceb1411.jpg",
                      Menu = "adicionales"
                  }
            };

            return productos;
        }

        public List<TamanioDto> ObtenerTamanios()
        {
            var tamanios = new List<TamanioDto>
            {
                new TamanioDto { Id = 30, Descripcion = "Mediana 30 cm", Precio = 199m },
                new TamanioDto { Id = 35, Descripcion = "Grande 35 cm", Precio = 229m },
                new TamanioDto { Id = 40, Descripcion = "Italiana 40 cm", Precio = 279m },
                new TamanioDto { Id = 45, Descripcion = "Dominator 45 cm", Precio = 379m }
            };
            return tamanios;
        }

        public List<MasaDto> ObtenerMasas()
        {
            var masas = new List<MasaDto>
            {
                new MasaDto { Id = 1, Nombre="Original",  Descripcion = "La original y clásica masa fresca hecha al momento con orilla dorada y espolvoreada de especias que le dan nuestro toque único."  },
                new MasaDto { Id = 2, Nombre= "Orilla rellena de queso", Descripcion = "Masa fresca hecha al momento con deliciosa orilla dorada rellena de queso mozzarella derretido y espolvoreada con especias." },
                new MasaDto { Id = 3, Nombre="Sartén", Descripcion = "Masa dorada y esponjosa con toque de mantequilla, doble capa de queso (provolone y mozzarella) e ingredientes hasta la orilla." },
                new MasaDto { Id = 4, Nombre="Crunchy", Descripcion = "Masa delgada y crujiente con borde dorado, perfecta para resaltar el sabor de los ingredientes y ofrecer una experiencia más ligera." }
            };
            return masas;
        }

        public List<ProductoDto> ObtenerBebidas()
        {
            var productos = new List<ProductoDto> {
                  new ProductoDto{
                    Id = 41,
                    Nombre = "Pepsi 600 ml",
                    Descripcion = "Experimenta todo el sabor cola, refrescando y amplificando cada momento (113 Cal) ",
                    Precio = 25,
                    Ruta = "images/bebidas/5090ae51-956d-42b0-a51a-cf5af5636032.jpg",
                    Menu = "bebidas"
                  },
                  new ProductoDto{
                    Id = 42,
                    Nombre = "Pepsi 1.5 litros",
                    Descripcion = "Experimenta todo el sabor cola, refrescando y amplificando cada momento (113 Cal) ",
                    Precio = 54,
                    Ruta = "images/bebidas/5090ae51-956d-42b0-a51a-cf5af5636032.jpg",
                    Menu = "bebidas"
                  },
                   new ProductoDto{
                    Id = 43,
                    Nombre = "Pepsi Ligth 600 ml",
                    Descripcion = "Experimenta y comparte el gran sabor cola, sin calorias (0 Cal) ",
                    Precio = 25,
                    Ruta = "images/bebidas/48dc6f3f-c5be-45fa-892e-c7064994f642.jpg",
                    Menu = "bebidas"
                  },
                  new ProductoDto{
                    Id = 44,
                    Nombre = "Pepsi Ligth 1.5 litros",
                    Descripcion = "Experimenta y comparte el gran sabor cola, sin calorias (0 Cal) ",
                    Precio = 54,
                    Ruta = "images/bebidas/48dc6f3f-c5be-45fa-892e-c7064994f642.jpg",
                    Menu = "bebidas"
                  },

                  new ProductoDto{
                    Id = 45,
                    Nombre = "7up 600 ml",
                    Descripcion = "Agua, burbujas y los refrescantes sabores naturales de la lima & limón (0 Cal)",
                    Precio = 32,
                    Ruta = "images/bebidas/0760521a-dcb5-4be7-b481-fa8b22ed8b88.jpg",
                    Menu = "bebidas"
                  },

                  new ProductoDto {
                    Id = 46,
                    Nombre = "7up 1.5 litros",
                    Descripcion = "Agua, burbujas y los refrescantes sabores naturales de la lima & limón (0 Cal).",
                    Precio = 54,
                    Ruta = "images/bebidas/0760521a-dcb5-4be7-b481-fa8b22ed8b88.jpg",
                    Menu = "bebidas"
                  },

                   new ProductoDto{
                    Id = 47,
                    Nombre = "E-Pura Natural 600 ml",
                    Descripcion = "Agua purificada que te hidrata y cuida tu corazón por no contener sodio (0 Cal) ",
                    Precio = 32,
                    Ruta = "images/bebidas/95f1a30b-62f8-4e4a-8f11-644e8a047e79.jpg",
                    Menu = "bebidas"
                  },
            };
            return productos;
        }
    }
}
