using Microsoft.AspNetCore.Mvc;
using ProductoBusinessLayer;
using ProductoBusinessLayer.Dtos;
using Utilidades.Api.Dtos;
using Utilidades.Api.Filters;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "cruds")]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoBl _productoBl;

        public ProductosController(ProductoBl productoBl)
        {
            _productoBl = productoBl;
        }

        /// <summary>
        /// Registro con apikey, se registra la apikey para su uso posterior en los endpoints de productos
        /// </summary>
        /// <returns></returns>        
        [HttpPost("Apikey")]
        public async Task<IActionResult> PostApiKey(ApikeyDtoIn apikeyDtoIn)
        {
            bool existe = await _productoBl.ExisteApikeyAsync(apikeyDtoIn.Apikey);
            if (existe)
            {
                return Conflict(new IdDto { Mensaje = "Apikey previamente registrada" });
            }
            await _productoBl.AgregarApiKeyAsync(apikeyDtoIn);

            return Created("", new ApikeyDto { Apikey = apikeyDtoIn.Apikey});
        }

        /// <summary>
        /// Lista de productos por apikey, se obtiene la lista de productos registrados con la apikey proporcionada en el header de la petición
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>                
        [ProducesResponseType(typeof(List<ProductoDto>), 200)]
        [Produces("application/json")]
        [HttpGet]
        [ApiKey]
        public async Task<IActionResult> Get([FromHeader] string apikey, bool activo= true)
        {            
            //bool existe = await _productoBl.ExisteApikeyAsync(apikey);
            //if (!existe)
            //    return Unauthorized();

            var lista = await _productoBl.ObtenerAsync(apikey,activo);

            this.HttpContext.Response.Headers.Add("total", lista.Count().ToString());

            return Ok(lista);
        }

        /// <summary>
        /// Registro con apikey, se registra la apikey para su uso posterior en los endpoints de productos
        /// </summary>
        /// <returns></returns>
        /// <response code="201"></response>     
        /// <response code="401">No autorizado</response>
        [ProducesResponseType(typeof(IdDto), 201)]
        [Produces("application/json")]
        [HttpPost]
        public async Task<IActionResult> Post([FromHeader] string apikey, ProductoDtoIn producto)
        {            
            bool existe = await _productoBl.ExisteApikeyAsync(apikey);
            if (!existe)
                return Unauthorized();
            var existentes = await _productoBl.ObtenerPorIdAsync(producto.EncodedKey);
            if (existentes is not null)
                return Conflict(new IdDto { Mensaje = "Producto previamente registrado" });
            var data = await _productoBl.AgregarAsync(producto, apikey);

            return Created("", new { FechaDeRegistro = DateTime.Now, EncodedKey = producto.EncodedKey });
        }


        /// <summary>
        /// Regresa el registro por el encodedKey
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>     
        /// <response code="401">No autorizado</response>
        /// <response code="404"></response>     
        [ProducesResponseType(typeof(IdDto), 201)]
        [ProducesResponseType(typeof(ProblemDetails), 401)]
        [ProducesResponseType(typeof(ProblemDetails), 404)]
        [Produces("application/json")]
        [HttpGet("{encodedKey}")]
        public async Task<IActionResult> GetById([FromHeader] string apikey, string encodedKey)
        {            
            bool existe = await _productoBl.ExisteApikeyAsync(apikey);
            if (!existe)
                return Unauthorized();

            var data = await _productoBl.ObtenerPorIdAsync(encodedKey);
            if (data == null)
                return NotFound(new { Mensaje = "No encontrado" });

            return Ok(data);
        }


        /// <summary>
        /// Actualizar producto
        /// </summary>
        /// <returns></returns>
        /// <response code="202"></response>     
        /// <response code="401">No autorizado</response>
        [ProducesResponseType(typeof(IdDto), 202)]
        [ProducesResponseType(typeof(ProblemDetails), 202)]
        [Produces("application/json")]
        [HttpPut("{encodedKey}")]
        public async Task<IActionResult> Put([FromHeader] string apikey, string encodedKey, ProductoDtoIn producto)
        {
            bool existe = await _productoBl.ExisteApikeyAsync(apikey);
            if (!existe)
                return Unauthorized();

            var data = await _productoBl.ObtenerPorIdAsync(encodedKey);
            if (data == null)
                return NotFound(new { Mensaje = "No encontrado" });

            await _productoBl.ActualizarAsync(encodedKey, producto);

            return Accepted(new IdDto { Mensaje = "Actualizado" });
        }

        [HttpDelete("{idEncodedKey}/Activos/{estaActivo}")]
        public async Task<IActionResult> Delete(string idEncodedKey, bool estaActivo)
        {

            await _productoBl.ActivarAsync(idEncodedKey, estaActivo);

            return NoContent();
        }

    }
}
