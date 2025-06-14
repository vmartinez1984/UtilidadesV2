using Microsoft.AspNetCore.Mvc;
using Notas.BussinesLayer;
using Notas.Dtos;
using System.ComponentModel.DataAnnotations;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Crud de notas
    /// </summary>
    /// <param name="notaBl"></param>
    [Route("api/Notas/V3")]
    [ApiController]
    public class NotasV3Controller : ControllerBase
    {
        private readonly NotaBl _notaBl;

        public NotasV3Controller(NotaBl notaBl)
        {
            _notaBl = notaBl;
            _notaBl.AgregarColeccion("NotasV3");
        }

        /// <summary>
        /// Obtener todas las notas
        /// </summary>
        /// <returns></returns>
        [HttpGet("{carpeta}")]
        public async Task<List<NotaDto>> ObtenerAsync([Required] string carpeta) => await _notaBl.ObtenerAsync(carpeta);

        /// <summary>
        /// Agregar nota
        /// </summary>
        /// <param name="nota"></param>
        /// <returns></returns>
        [HttpPost("{carpeta}")]
        public async Task<IActionResult> AgregarAsync([Required] string carpeta, NotaDto nota) => Ok(new { Id = await _notaBl.AgregarAsync( nota, carpeta) });

        /// <summary>
        /// Actualizar nota
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nota"></param>
        /// <returns></returns>
        [HttpPut("{carpeta}")]
        public async Task<IActionResult> ActualizarAsync([Required] string carpeta, string id, NotaDtoIn nota)
        {
            await _notaBl.Actaulizar(id, nota);

            return Accepted();
        }        
    }
}
