using Contador.BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Api.Dtos;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ContadoresController : ControllerBase
    {
        private readonly ContadorService _service;

        public ContadoresController(ContadorService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ContadorDto dto)
        {
            var contador = await _service.Crear(dto);

            return Ok(contador);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            var valor = await _service.GetActual(key);

            if (valor == null)
                return NotFound();

            return Ok(new IdDto { Id = valor.ToString() });
        }

        [HttpPut("{key}")]
        public async Task<IActionResult> Next(string key)
        {
            var valor = await _service.Next(key);

            if (valor == null)
                return NotFound();

            return Created("/key",new IdDto { Id = valor.ToString() });
        }
    }
}
