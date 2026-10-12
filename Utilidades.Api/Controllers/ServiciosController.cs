
using ImplementacionQr;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asistencias")]
    public class ServiciosController : ControllerBase
    {
        /// <summary>
        /// Generación de un QR a partir de una cadena
        /// </summary>
        /// <param name="mensaje"></param>
        /// <param name="ancho"></param>
        /// <param name="largo"></param>
        /// <param name="margen"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Qr([MaxLength(2048)]string mensaje, int ancho = 400, int largo = 400, int margen = 10)
        {
            ServicioDeQr servicioDeQr = new ServicioDeQr();
            var qrCodeBytes = servicioDeQr.GenerateQrCode(mensaje);

            return File(qrCodeBytes, "image/png");
        }
    }
}
