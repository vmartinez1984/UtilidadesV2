
using ImplementacionQr;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiciosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Qr([MaxLength(2048)]string mensaje, int ancho = 400, int largo = 400, int margen = 10)
        {
            ServicioDeQr servicioDeQr = new ServicioDeQr();
            var qrCodeBytes = servicioDeQr.GenerateQrCode(mensaje);

            return File(qrCodeBytes, "image/png");
        }
    }
}
