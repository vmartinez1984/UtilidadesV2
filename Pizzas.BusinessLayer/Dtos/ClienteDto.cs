using System.ComponentModel.DataAnnotations;

namespace VMtz84.Pizzas.Dtos
{
    public class ClienteDto
    {
        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(64)]
        public string Nombre { get; set; }

        [Required]
        [MaxLength(64)]
        public string Apellidos { get; set; }

        [Required]
        [MaxLength(64)]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        [MaxLength(12)]
        public string Contrasenia { get; set; }
        public string NombreCompleto { get { return $"{Nombre} {Apellidos}"; } }
    }

    public class DireccionDto
    {
        public string Encodekey { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string UsuarioEncodedkey { get; set; }

        public bool EsLaPrincipal { get; set; }
    }
}
