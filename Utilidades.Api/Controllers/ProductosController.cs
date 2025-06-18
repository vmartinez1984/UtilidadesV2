using Microsoft.AspNetCore.Mvc;
using ProductoBusinessLayer;
using System.ComponentModel.DataAnnotations;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly ProductoBl _productoBl;

        public ProductosController(ProductoBl productoBl)
        {
            _productoBl = productoBl;
        }

        [HttpGet("/Llaves/{llave}")]
        public async Task<IActionResult> Get([Required] string llave)
        {
            var lista = await _productoBl.ObtenerAsync(llave);
            this.HttpContext.Response.Headers.Add("total", lista.Count().ToString());

            return Ok(lista);
        }

        [HttpPost("{Llave}")]
        public async Task<IActionResult> Post(ProductoDto producto, [Required] string llave)
        {
            var data = await _productoBl.AgregarAsync(producto, llave);

            return Created("", new { FechaDeRegistro = DateTime.Now, Id = data, EncodedKey = producto.EncodedKey });
        }

        [HttpGet("{idEncodedKey}")]
        public async Task<IActionResult> GetById(string idEncodedKey)
        {
            var data = await _productoBl.ObtenerPorIdAsync(idEncodedKey);
            if (data == null)
                return NotFound(new { Mensaje = "No encontrado" });
            return Ok(data);
        }

        [HttpPut("{idEncodedKey}")]
        public async Task<IActionResult> Put(string idEncodedKey, ProductoDto producto)
        {
            await _productoBl.ActualizarAsync(idEncodedKey, producto);

            return NoContent();
        }

        [HttpDelete("{idEncodedKey}/Activos/{estaActivo}")]
        public async Task<IActionResult> Delete(string idEncodedKey, bool estaActivo)
        {
            await _productoBl.ActivarAsync(idEncodedKey, estaActivo);

            return NoContent();
        }
    }
}
