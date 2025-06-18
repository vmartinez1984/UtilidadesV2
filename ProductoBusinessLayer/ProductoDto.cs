using System.ComponentModel.DataAnnotations;

namespace ProductoBusinessLayer
{
    public class ProductoDto
    {
        public string EncodedKey { get; set; } = Guid.NewGuid().ToString();               

        [Required]
        public string Valor01 { get; set; }

        public string Valor02 { get; set; }

        public string Valor03 { get; set; }

        public string Valor04 { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public bool EstaActivo { get; set; }
    }
}
