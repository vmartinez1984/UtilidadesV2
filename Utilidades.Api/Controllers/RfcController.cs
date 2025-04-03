using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios.Dtos;

namespace Utilidades.Api.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    //public class RfcController : ControllerBase
    //{
    //    /// <summary>
    //    /// Genera el rfc con homoclave, deacuedo con el algoritmo del SAT.
    //    /// Fisica = 0, Moral = 1
    //    /// </summary>
    //    /// <param name="rfcDto"></param>
    //    /// <returns></returns>
    //    /// <response code="202">Create</response>
    //    [HttpPost]
    //    [ProducesResponseType(typeof(string), 202)]
    //    [Produces("application/json")]
    //    public ActionResult GenerarRfcConHomoclave(
    //        RfcDto rfcDto
    //    )
    //    {
    //        string rfcConHomoclave;

    //        rfcConHomoclave = RfcServicio.CalculateRFCHomonym(
    //            rfcDto.TipoDePersona,
    //            rfcDto.Nombre,
    //            rfcDto.PrimerApellido,
    //            rfcDto.SegundoApellido,
    //            rfcDto.FechaDeNacimiento
    //        );

    //        return Created(string.Empty, new { Rfc = rfcConHomoclave, Fecha= DateTime.Now });
    //    }
    //}
}
