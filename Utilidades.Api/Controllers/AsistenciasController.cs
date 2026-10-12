using JwtToken.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Api.Filters;
using VMtz84.Asistencias.Dtos;
using VMtz84.Asistencias.ReglasDeNegocio;
using VMtz84.Pizzas.Dtos;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Controlador para probar una ap de asistencia
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Asistencias")]
    [AllowAnonymous]
    public class AsistenciasController : ControllerBase
    {
        private readonly UsuarioReglaDeNegocio _service;
        private readonly AsistenciaReglasDeNegocio _asistenciaReglasDeNegocio;
        private JwtTokenService _jwtTokenService;

        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioService"></param>
        /// <param name="asistenciaReglasDeNegocio"></param>
        /// <param name="jwtTokenService"></param>
        /// <param name="environment"></param>
        public AsistenciasController(
            UsuarioReglaDeNegocio usuarioService,
            AsistenciaReglasDeNegocio asistenciaReglasDeNegocio,
            JwtTokenService jwtTokenService,
            IWebHostEnvironment environment
        )
        {
            _service = usuarioService;
            this._asistenciaReglasDeNegocio = asistenciaReglasDeNegocio;
            _jwtTokenService = jwtTokenService;
            _environment = environment;
        }

        /// <summary>
        /// Registro de usuarios
        /// </summary>
        /// <returns></returns>
        /// <response code="201"></response>        
        [ProducesResponseType(typeof(List<IdDto>), 201)]
        [Produces("application/json")]
        [HttpPost("/Usuarios")]
        public async Task<IActionResult> AgregarAsync(UsuarioDtoIn usuario)
        {
            var existeCorreo = await _service.ExiteCorreoRegistradoPreviamente(usuario.Correo);
            if (existeCorreo)
                return BadRequest(new ProblemDetails { Detail = "Correo registrado previamente" });
            var id = await _service.AgregarAsync(usuario);

            return Created(string.Empty, new IdDto { Id = id, Mensaje = "Usuario registrado" });
        }

        /// <summary>
        /// Autorización Basica para obtener token JWT
        /// </summary>        
        /// <returns></returns>
        /// <response code="200"></response>        
        /// <response code="404"></response>        
        [ProducesResponseType(typeof(TokenDto), 200)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [Produces("application/json")]
        [HttpGet("/Usuarios/Sesiones")]
        public async Task<IActionResult> ObtenerTokenAsync([FromQuery] InicioDeSesionDto inicioDeSesionDto)
        {
            var cliente = await _service.ObtenerAsync(inicioDeSesionDto);
            if (cliente == null)
                return NotFound(new ProblemDetails { Status = 404, Detail = "Datos incorrectos" });
            var fechaDeExpiracion = DateTime.UtcNow.AddMinutes(20);
            var token = _jwtTokenService.ObtenerToken(cliente.NombreCompleto, "Cliente", cliente.Id.ToString(), cliente.Correo, fechaDeExpiracion);

            return Ok(new { Token = token, Expiracion = fechaDeExpiracion });
        }

        /// <summary>
        /// Registro de asistencia
        /// </summary>        
        /// <returns></returns>
        /// <response code="201"></response>                 
        [ProducesResponseType(typeof(TokenDto), 201)]
        [HttpPost("/Usuarios/Asistencias")]
        [BearerAuth]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Cliente")]
        public async Task<IActionResult> RegsitrarAsistenciaAsync([FromForm] AsistenciaDtoIn asistenciaIn)
        {
            try
            {
                var asistencia = asistenciaIn.ToAsistenciaDto2In();
                var claim = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ClienteId");
                var usuarioId = int.Parse(claim.Value);
                asistencia.UsuarioId = usuarioId;
                string webRootPath = _environment.WebRootPath ?? Path.Combine(_environment.ContentRootPath, "wwwroot");
                asistencia.Ruta = webRootPath;
                var id = await _asistenciaReglasDeNegocio.AgregarAsync(asistencia);

                return Created(string.Empty, new IdDto { Id = id, Mensaje = "Asistencia registrada" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Lista de asistencia
        /// </summary>        
        /// <returns></returns>
        /// <response code="200"></response>                 
        [ProducesResponseType(typeof(TokenDto), 200)]
        [HttpGet("/Usuarios/Asistencias")]
        [BearerAuth]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Cliente")]
        public async Task<IActionResult> ObtenerListaDeAsistencias()
        {
            var claim = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ClienteId");
            var usuarioId = int.Parse(claim.Value);
            var lista = await _asistenciaReglasDeNegocio.ObtenerListaPorUsuarioIdAsync(usuarioId);          

            return Ok(lista);
        }

        /// <summary>
        /// Lista de eventos
        /// </summary>        
        /// <returns></returns>
        /// <response code="200"></response>                 
        [ProducesResponseType(typeof(TokenDto), 200)]
        [HttpGet("/Eventos")]
        public async Task<IActionResult> ObtenerEventosAsync()
        {
            List<EventoDto> lista = new List<EventoDto> {
                new EventoDto { Id=1, Nombre = "Evento chido", Descripcion = "Algo chido", Fecha = new DateTime(2026, 10, 11, 18,00,0)},
                new EventoDto { Id= 2, Nombre = "Mentiras el musical", Descripcion = "Obra de teatro", Fecha = new DateTime(2026, 10, 12, 18,00,0)}
            };

            return Ok(lista);
        }
    }
}
