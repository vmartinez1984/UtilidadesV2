using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notas.BussinesLayer;
using Notas.Dtos;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Crud de notas
    /// </summary>
    /// <param name="notaBl"></param>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Yo merengues")]
    public class NotasController(NotaBl notaBl) : ControllerBase
    {
        private readonly NotaBl _notaBl = notaBl;

        /// <summary>
        /// Obtener todas las notas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<NotaDto>> ObtenerAsync() => await _notaBl.ObtenerAsync();

        /// <summary>
        /// Agregar nota
        /// </summary>
        /// <param name="nota"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AgregarAsync(NotaDto nota) => Ok(new { Id = await _notaBl.AgregarAsync(nota) });

        /// <summary>
        /// Actualizar nota
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nota"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> ActualizarAsync(string id, NotaDtoIn nota)
        {
            await _notaBl.Actaulizar(id, nota);

            return Accepted();
        }        
    }
}
