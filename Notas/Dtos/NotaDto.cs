using System.ComponentModel.DataAnnotations;

namespace Notas.Dtos
{   
    public class NotaDto: NotaDtoIn
    {
        [Required]
        public string EncodedKey { get; set; } = Guid.NewGuid().ToString();
    }

    public class NotaDtoIn
    {
        [Required]
        [MaxLength(100)]
        public string Tags { get; set; }

        [Required]
        [MaxLength(2048)]
        public string Valor01 { get; set; }

        [Required]
        [MaxLength(100)]
        public string Valor02 { get; set; }

        [MaxLength(100)]
        public string Valor03 { get; set; }

        [MaxLength(100)]
        public string Valor04 { get; set; }        
    }
}
