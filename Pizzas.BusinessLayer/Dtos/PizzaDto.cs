using System.ComponentModel.DataAnnotations;

namespace VMtz84.Pizzas.Dtos
{
    public class ProductoDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Ingredientes { get; set; }
        public string Ruta { get; set; }
        public string Menu { get; internal set; }
        public decimal Precio { get; internal set; }
    }

    public class MasaDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }

    public class TamanioDto
    {
        public int Id { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
    }

    public class PizzaDtoIn
    {
        [Required]
        public int PizzaId { get; set; }

        public int Pizza2Id { get; set; }

        [Required]
        public int MasaId { get; set; }

        [Required]
        public string Tamanio { get; set; }
    }

    public class PizzaDto
    {
        public int PizzaId { get; set; }
        public int Pizza2Id { get; set; }
        public string Nombre1 { get; set; }
        public string Nombre2 { get; set; }        
        public string Tamanio { get; set; }
        public string Masa { get; set; }
        public decimal Precio { get; set; }
    }
}
