namespace Utilidades.Api.Dtos
{
    public class InicioDeSesionDto
    {
        public string Usuario { get; set; }

        public string Contraseña { get; set; }
    }

    public class TokenDto
    {
        public string Token { get; set; }

        public DateTime FechaDeExpiracion { get; set; }
    }
}
