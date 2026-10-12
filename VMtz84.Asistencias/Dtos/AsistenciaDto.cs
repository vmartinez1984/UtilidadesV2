using Microsoft.AspNetCore.Http;

namespace VMtz84.Asistencias.Dtos
{
    public class AsistenciaDto
    {        
        public string Gps { get; set; }
        public string UrlSelfie { get; set; }        

        private DateTime fechaDeRegistro;

        public DateTime FechaDeRegistro
        {
            get { return fechaDeRegistro.ToLocalTime(); }
            set { fechaDeRegistro = value; }
        }

    }

    public class AsistenciaDtoIn
    {       
        public string Gps { get; set; }
        public IFormFile Selfie { get; set; }
    }

    public static class Mapeador
    {
        public static AsistenciaDto2In ToAsistenciaDto2In(this AsistenciaDtoIn asistencia)
        {
            return new AsistenciaDto2In
            {
                Gps = asistencia.Gps,
                Selfie = asistencia.Selfie,
            };
        }
    }

    public class AsistenciaDto2In
    {        
        public int UsuarioId { get; set; }
        public string Ruta { get; set; }
        public string Gps { get; set; }
        public IFormFile Selfie { get; set; }
    }
}
