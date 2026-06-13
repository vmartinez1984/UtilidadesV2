using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VMtz84.Pizzas.Dtos
{
    public class OrdenDtoIn
    {
        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public List<ProductoDto> Productos { get; set; }

        [Required]
        public List<PizzaDtoIn> Pizzas { get; set; }

        [Required]
        public string MetodoDePago { get; set; }

        [JsonIgnore]
        public string ClienteEncodedkey { get; set; }
    }

    public class OrdenDto
    {
        public List<ProductoDto> Productos { get; set; }

        public List<PizzaDto> Pizzas { get; set; }

        public string MetodoDePago { get; set; }

        public string Estado { get; set; }
    }
}
