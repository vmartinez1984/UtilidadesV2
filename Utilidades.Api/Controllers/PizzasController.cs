using JwtToken.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Utilidades.Api.Dtos;
using Utilidades.Api.Filters;
using VMtz84.Pizzas.Dtos;
using VMtz84.Pizzas.Services;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Provides API endpoints for retrieving menu categories, pizzas, sizes, dough types, chicken products,
    /// additionals, beverages, and for managing client information in the pizza ordering application.
    /// </summary>
    /// <remarks>This controller exposes RESTful endpoints for accessing menu data and client operations. All
    /// endpoints return JSON responses and are grouped under the 'v1' API version. Client management endpoints support
    /// adding new clients and are designed to facilitate integration with authentication mechanisms. Endpoints are
    /// intended for use in web and mobile applications interacting with the pizza ordering system.</remarks>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Pizzas")]
    [AllowAnonymous]
    public class PizzasController : ControllerBase
    {
        PizzaService _service;
        private JwtTokenService _jwtTokenService;

        /// <summary>
        /// Initializes a new instance of the PizzasController class with the specified pizza and JWT token services.
        /// </summary>
        /// <param name="pizzaService">The service used to manage pizza-related operations.</param>
        /// <param name="jwtTokenService">The service used to handle JWT token generation and validation.</param>
        public PizzasController(PizzaService pizzaService, JwtTokenService jwtTokenService)
        {
            _service = pizzaService;
            _jwtTokenService = jwtTokenService;
        }

        /// <summary>
        /// Retrieves the list of available menu categories for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<MenuDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Menus")]
        public IActionResult GetMenu() => Ok(_service.Menu.ObtenerMenus());


        /// <summary>
        /// Retrieves the list of available pizzas for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pizzas")]
        public IActionResult GetPizzas() => Ok(_service.Menu.ObtenerPizzas());


        /// <summary>
        /// Retrieves the list of available sizes for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<TamanioDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pizzas/Tamanios")]
        public IActionResult GetTamanios() => Ok(_service.Menu.ObtenerTamanios());

        /// <summary>
        /// Retrieves the list of available mass for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<MasaDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pizzas/Masas")]
        public IActionResult GetMasas() => Ok(_service.Menu.ObtenerMasas());


        /// <summary>
        /// Retrieves the list of available chickens for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pollos")]
        public IActionResult GetPollos() => Ok(_service.Menu.ObtenerPollos());


        /// <summary>
        /// Retrieves the list of available additionals for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Adicionales")]
        public IActionResult GetAdicionales() => Ok(_service.Menu.ObtenerAdicionales());


        /// <summary>
        /// Retrieves the list of available beberages for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Bebidas")]
        public IActionResult GetBebidas() => Ok(_service.Menu.ObtenerBebidas());

        /// <summary>
        /// Add Client
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Client already added</response>        
        /// <response code="201">Created</response>
        [ProducesResponseType(typeof(IdDto), 200)]
        [ProducesResponseType(typeof(IdDto), 201)]
        [Produces("application/json")]
        [HttpPost("Clientes")]
        public async Task<IActionResult> AgregarClienteAsync(ClienteDto cliente)
        {
            var clienteAnterior = await _service.Cliente.ObtenerPorCorreoAsync(cliente.Correo);
            if (clienteAnterior is not null)
                return Ok(new IdDto { Id = clienteAnterior.Encodedkey });

            var data = await _service.Cliente.AgregarAsync(cliente);

            return Created("", new IdDto { Id = cliente.Encodedkey });
        }

        /// <summary>
        /// Autorización Basica para obtener token JWT
        /// </summary>        
        /// <returns></returns>
        [HttpPost("Clientes/InicioDeSesiones")]
        [BasicAuth]
        public async Task<IActionResult> ObtenerTokenAsync()
        {
            this.HttpContext.Request.Headers.TryGetValue("Authorization", out var credenciales);
            if (string.IsNullOrEmpty(credenciales))
                return BadRequest(new ProblemDetails { Title = "Credenciales no proporcionadas", Detail = "El encabezado 'Authorization' es requerido para iniciar sesión." });

            var credentialsBytes = Convert.FromBase64String(credenciales.ToString().Replace("Basic ", string.Empty));
            var credentials = Encoding.UTF8.GetString(credentialsBytes).Split(':', 2);
            if (credentials.Length != 2)
                return BadRequest(new ProblemDetails { Title = "Credenciales inválidas", Detail = "Formato de credenciales inválido." });

            var correo = credentials[0];
            var contraseña = credentials[1];
            var esValido = await _service.Cliente.ValidarCredencialesAsync(correo, contraseña);
            if (!esValido)
                return Unauthorized(new ProblemDetails { Title = "Credenciales inválidas", Detail = "El correo o la contraseña proporcionados son incorrectos." });

            var clienteAnterior = await _service.Cliente.ObtenerPorCorreoAsync(correo);
            var fechaDeExpiracion = DateTime.UtcNow.AddMinutes(20);
            var token = _jwtTokenService.ObtenerToken(clienteAnterior.NombreCompleto, "Cliente", clienteAnterior.Encodedkey, clienteAnterior.Correo, fechaDeExpiracion);

            return Ok(new { Token = token, Expiracion = fechaDeExpiracion });
        }

        /// <summary>
        /// Registro de orden para cliente autenticado
        /// </summary>
        /// <param name="ordenDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost("Clientes/Ordenes")]
        [ProducesResponseType(typeof(IdDto), 201)]
        [Produces("application/json")]
        [BearerAuth]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Cliente")]
        public async Task<IActionResult> AgregarOrdenAsync(OrdenDtoIn ordenDto)
        {
            var claim = this.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "ClienteId");
            ordenDto.ClienteEncodedkey = claim.Value;

            var idDto = await _service.Orden.AgregarAsync(ordenDto);

            return Created("", new IdDto { Id = idDto });
        }

        /// <summary>
        /// Obtener orden de cliente
        /// </summary>
        /// <param name="ordenDto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>        
        [ProducesResponseType<OrdenDto>(201)]
        [Produces("application/json")]
        [HttpGet("Clientes/Ordenes")]
        [BearerAuth]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Cliente")]
        public async Task<IActionResult> ObtenerOrdenAsync()
        {
            var claim = this.HttpContext.User.Claims.First(x => x.Type == "ClienteId");
            var ordenDto = await _service.Orden.ObtenerOrdenPorCliente(claim.Value);

            return Ok(ordenDto);
        }
    }
}