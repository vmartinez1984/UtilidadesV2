using System.ComponentModel.DataAnnotations;

namespace Notas.Dtos
{
    public class NotaUpdateDto 
    {
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Contenido { get; set; }

        [Required]
        [MaxLength(100)]
        public string Tags { get; set; }
                
        [MaxLength(50)]
        public string Estado { get; set; }

        public DateTime? FechaInicio { get; set; }
                
        public DateTime? FechaFin { get; set; }
    }

    public class NotaDto: NotaUpdateDto
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
