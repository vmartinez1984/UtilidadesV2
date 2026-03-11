using JwtToken.Services;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Api.Dtos;
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
    [ApiExplorerSettings(GroupName = "v1")]
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
        public IActionResult GetMenu() => Ok(_service.ObtenerMenus());


        /// <summary>
        /// Retrieves the list of available pizzas for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pizzas")]
        public IActionResult GetPizzas() => Ok(_service.ObtenerPizzas());


        /// <summary>
        /// Retrieves the list of available sizes for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<TamanioDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pizzas/Tamanios")]
        public IActionResult GetTamanios() => Ok(_service.ObtenerTamanios());

        /// <summary>
        /// Retrieves the list of available mass for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<MasaDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pizzas/Masas")]
        public IActionResult GetMasas() => Ok(_service.ObtenerMasas());


        /// <summary>
        /// Retrieves the list of available chickens for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Pollos")]
        public IActionResult GetPollos() => Ok(_service.ObtenerPollos());


        /// <summary>
        /// Retrieves the list of available additionals for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Adicionales")]
        public IActionResult GetAdicionales() => Ok(_service.ObtenerAdicionales());


        /// <summary>
        /// Retrieves the list of available beberages for the application.
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet("Bebidas")]
        public IActionResult GetBebidas() => Ok(_service.ObtenerBebidas());

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
                return Ok(new Dtos.IdDto { Id = clienteAnterior.Encodedkey });

            var data = await _service.Cliente.AgregarAsync(cliente);

            return Created("", new Dtos.IdDto { Id = cliente.Encodedkey });
        }

        /// <summary>
        /// No implementado
        /// </summary>        
        /// <returns></returns>
        [HttpPost("Clientes/InicioDeSesiones")]
        public async Task<IActionResult> AgregarClienteAsync()
        {
            throw new NotImplementedException("Este endpoint no está implementado. Se requiere un mecanismo de autenticación adecuado para manejar el inicio de sesión de los clientes.");
            this.HttpContext.Request.Headers.TryGetValue("Authorization", out var credenciales);
            if (string.IsNullOrEmpty(credenciales))
                return BadRequest(new ProblemDetails { Title = "Credenciales no proporcionadas", Detail = "El encabezado 'Authorization' es requerido para iniciar sesión." });

            var correo = "";
            var contraseña = "";
            var esValido = await _service.Cliente.ValidarCredencialesAsync(correo, contraseña);
            if(!esValido)
                return Unauthorized(new ProblemDetails { Title = "Credenciales inválidas", Detail = "El correo o la contraseña proporcionados son incorrectos." });

            var clienteAnterior = await _service.Cliente.ObtenerPorCorreoAsync(correo);
            var fechaDeExpiracion = DateTime.UtcNow.AddMinutes(20);
            var token = _jwtTokenService.ObtenerToken(clienteAnterior.NombreCompleto, "Cliente", clienteAnterior.Encodedkey, clienteAnterior.Correo, fechaDeExpiracion);

            return Ok(new { Token = token, Expiracion = fechaDeExpiracion });
        }
    }
}