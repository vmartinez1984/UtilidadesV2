using System.ComponentModel.DataAnnotations;

namespace VMtz84.Asistencias.Dtos
{
    public class UsuarioDtoIn
    {
        [Required, MaxLength(128), MinLength(6)]
        public string Nombre { get; set; }

        [Required, MaxLength(128), MinLength(6)]
        public string Apellidos { get; set; }

        [Required, MaxLength(64), MinLength(6), EmailAddress]
        public string Correo { get; set; }

        [Required, MaxLength(16), MinLength(6)]
        public string Contrasenia { get; set; }
    }

    public class UsuarioDto
    {        
        public string Nombre { get; set; }                
        public string Correo { get; set; }
        public string Apellidos { get;  set; }
        public string NombreCompleto { get { return $"{Nombre} {Apellidos}"; } }
        public int Id { get; set; }
    }
}
