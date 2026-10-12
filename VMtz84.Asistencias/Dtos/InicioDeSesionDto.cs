using System.ComponentModel.DataAnnotations;

namespace VMtz84.Asistencias.Dtos
{
    public class InicioDeSesionDto
    {
        [Required, MaxLength(64), MinLength(6), EmailAddress]
        public string Correo { get; set; }

        [Required, MaxLength(16), MinLength(6)]
        public string Contrasenia { get; set; }
    }
}
