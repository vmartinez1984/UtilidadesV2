using JwtTokenService;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Api.Dtos;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Inicio de sesión para notas
    /// </summary>
    /// <param name="jwtToken"></param>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Notas")]
    public class InicioDeSesionesController(JwtToken jwtToken) : ControllerBase
    {
        private readonly JwtToken _jwtToken = jwtToken;

        /// <summary>
        /// Retorna token
        /// </summary>
        /// <param name="inicioDeSesion"></param>
        /// <returns></returns>
        [HttpPost]        
        [ProducesResponseType<TokenDto>(200)]        
        [ProducesResponseType<ProblemDetails>(404)]        
        public IActionResult IniciarSesion(InicioDeSesionDto inicioDeSesion)
        {
            if (inicioDeSesion.Usuario == "ahal_tocob@hotmail.com" && inicioDeSesion.Contraseña == "Macross#2012")
            {
                var fechaDeExpiracion = DateTime.Now.AddMinutes(20);
                var token = _jwtToken.ObtenerToken(
                    "Víctor Mtz",
                    "Yo merengues",
                    "2025",
                    "ahal_tocob@hotmail.com",
                    fechaDeExpiracion
                );

                return Ok(new TokenDto
                {
                    FechaDeExpiracion = fechaDeExpiracion,
                    Token = token
                });
            }

            return NotFound(new ProblemDetails { Detail = "No nimergas"});
        }
    }
}
