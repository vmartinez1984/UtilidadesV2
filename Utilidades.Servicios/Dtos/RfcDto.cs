using System.ComponentModel.DataAnnotations;
using Utilidades.Servicios.Rfc;

namespace Utilidades.Servicios.Dtos
{
    public class RfcDto
    {
        [Required]
        public TipoPersona TipoDePersona { get; set; } = TipoPersona.Fisica;

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string PrimerApellido { get; set; }

        [Required]
        public string SegundoApellido { get; set; }

        [Required]
        public DateTime FechaDeNacimiento { get; set; }
    }
}
