using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Notas.BussinesLayer;
using Notas.Dtos;
using Utilidades.Api.Dtos;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Crud de notas
    /// </summary>    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Yo merengues")]
    [ApiExplorerSettings(GroupName = "Notas")]
    public class NotasController : ControllerBase
    {
        private readonly NotaBl _notaBl;

        public NotasController(NotaBl notaBl)
        {
            _notaBl = notaBl;            
        }

        /// <summary>
        /// Obtener todas las notas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType<List<NotaDto>>(200)]
        [Produces("application/json")]
        public async Task<List<NotaDto>> ObtenerAsync() => await _notaBl.ObtenerAsync();

        /// <summary>
        /// Agregar nota
        /// </summary>
        /// <param name="nota"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType<IdDto>(201)]
        [Produces("application/json")]
        public async Task<IActionResult> AgregarAsync(NotaDto nota) => Created(string.Empty, new { Id = await _notaBl.AgregarAsync(nota) });

        /// <summary>
        /// Actualizar nota
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nota"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(202)]
        public async Task<IActionResult> ActualizarAsync(string id, NotaDtoIn nota)
        {
            await _notaBl.Actaulizar(id, nota);

            return Accepted();
        }        
    }   
}
