using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ProductoBusinessLayer.Dtos
{
    public class ProductoDto
    {
        public string EncodedKey { get; set; } = Guid.NewGuid().ToString();               

        [Required]
        [MaxLength(256)]
        public string Valor01 { get; set; }

        [MaxLength(512)]
        public string Valor02 { get; set; }

        [MaxLength(512)]
        public string Valor03 { get; set; }

        [MaxLength(512)]
        public string Valor04 { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public bool EstaActivo { get; set; } = true;
    }

    public class ProductoDtoIn
    {
        public string EncodedKey { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(256)]
        public string Valor01 { get; set; }

        [MaxLength(512)]
        public string Valor02 { get; set; }

        [MaxLength(512)]
        public string Valor03 { get; set; }

        [MaxLength(512)]
        public string Valor04 { get; set; }
    }

    public class ApikeyDtoIn
    {
        [MaxLength(256)]
        public string Apikey { get; set; } = ObjectId.GenerateNewId().ToString();

        [MaxLength(256)]
        public string Nombre { get; set; }

        [MaxLength(256)]
        public string Correo { get; set; }

        [MaxLength(256)]
        public string Nota { get; set; }

    }

    public class ApikeyDto {
        public string Apikey { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
            
    
}
