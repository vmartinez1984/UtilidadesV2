﻿using CodigosPostales.ReglasDeNegocio;
using CodigosPostales.Repositorios;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Contralador de codigos postales
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CodigosPostalesController : ControllerBase
    {
        private readonly ICodigoPostalRdn _repositorio;

        /// <summary>
        /// Constructor
        /// </summary>        
        /// <param name="repositorio"></param>
        public CodigosPostalesController(ICodigoPostalRdn repositorio)
        {
            _repositorio = repositorio;
        }

        /// <summary>
        /// Lista de estados
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet("Estados",Name = "Estados")]
        [ProducesResponseType(typeof(List<Estado>), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> ObtenerEstadosAsync()
        {
            var lista = await _repositorio.ObtenerEstadosAsync();
            HttpContext.Response.Headers.Append("Total", lista.Count.ToString());

            return Ok(lista.OrderBy(x => x.Nombre));
        }

        /// <summary>
        /// Lista de municipios por estado
        /// </summary>
        /// <param name="estado"></param>        
        /// <returns></returns>
        /// <response code="200"></response>
        [HttpGet("{estado}/Alcaldias")]
        [ProducesResponseType(typeof(List<Alcaldia>), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> ObtenerAlcaldias(string estado)
        {
            var lista = await _repositorio.ObtenerAlcaldiasAsync(estado);
            HttpContext.Response.Headers.Append("Total", lista.Count.ToString());

            return Ok(lista);
        }
        /// <summary>
        /// Códigos por estado y alcaldia
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="alcaldia"></param>
        /// <returns>Codigos postales</returns>
        [HttpGet("Estados/{estado}/Alcaldias/{alcaldia}")]
        [ProducesResponseType(typeof(List<CodigoPostalDto>), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> ObtenerCodigosPostalesPorAlcaldia(string estado, string alcaldia)
        {
            var lista = await _repositorio.ObtenerCodigosPostalesAsync(estado, alcaldia);

            HttpContext.Response.Headers.Append("Total", lista.Count.ToString());
            return Ok(lista);
        }

        /// <summary>
        /// Obtener la lista de codigos postales
        /// </summary>        
        /// <returns></returns>         
        [HttpGet("{codigoPostal}")]
        [ProducesResponseType(typeof(List<CodigoPostalDto>), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> ObtenerCodigosPostales([StringLength(5, MinimumLength = 5)] string codigoPostal)
        {
            var lista = await _repositorio.ObtenerCodigosPostalesAsync(codigoPostal);
            HttpContext.Response.Headers.Append("Total", lista.Count.ToString());

            return Ok(lista);
        }

        /// <summary>
        /// Obtener los codigos psotales a partir del nombre de una colonia
        /// </summary>
        /// <param name="asentamiento"></param>
        /// <returns></returns>
        [HttpGet("{asentamiento}/Buscar")]
        [ProducesResponseType(typeof(List<CodigoPostalDto>), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> ObtenerCodigosPostalesPorAsentamientamiento(string asentamiento)
        {
            var lista = await _repositorio.BuscarCodigosPostalesAsync(asentamiento);
            HttpContext.Response.Headers.Append("Total", lista.Count.ToString());

            return Ok(lista);
        }

        /// <summary>
        /// Obtener los codigos psotales a partir del nombre de una colonia
        /// </summary>
        /// <param name="estado"></param>
        /// <param name="alcaldia"></param>
        /// <param name="asentamiento"></param>
        /// <returns></returns>
        [HttpGet("Estados/{estado}/Alcaldias/{alcaldia}/{asentamiento}/Buscar")]
        [ProducesResponseType(typeof(List<CodigoPostalDto>), 200)]
        [Produces("application/json")]
        public async Task<List<CodigoPostalDto>> ObtenerCodigosPostalesAsync(string estado, string alcaldia, string asentamiento)
        {
            var lista = await _repositorio.ObtenerCodigosPostalesAsync(estado, alcaldia, asentamiento);
            HttpContext.Response.Headers.Append("Total", lista.Count.ToString());

            return lista;
        }

        /// <summary>
        /// Obtiene un codigo postal aleatorio
        /// </summary>
        /// <returns></returns>
        [HttpGet("Aleatorio")]
        [ProducesResponseType(typeof(CodigoPostalDto), 200)]
        [Produces("application/json")]
        public async Task<CodigoPostalDto> ObtenerCodigoPostalAleatorio()
        {
            CodigoPostalDto x;

            x = await _repositorio.ObtenerCodigoPostalAleatorioAsync();

            return x;
        }

        /// <summary>
        /// Obtiene un codigo postal aleatorio
        /// </summary>
        /// <returns></returns>
        [HttpGet("Estados/{estado}/Aleatorio")]
        [ProducesResponseType(typeof(CodigoPostalDto), 200)]
        [Produces("application/json")]
        public async Task<IActionResult> ObtenerCodigoPostalAleatorioPorEstadoAsync(string estado)
        {
            int estadoId;

            if (int.TryParse(estado, out estadoId))
            {
                if (estadoId > 0 && estadoId >= 33)
                {
                    return BadRequest(new { Menseje = "El estadoId debe de ser de 1 a 32" });
                }

            }
            CodigoPostalDto x;

            x = await _repositorio.ObtenerCodigoPostalAleatorioAsync(estado);

            return Ok(x);
        }

        /// <summary>
        /// Subir coleccion de codigos postales
        /// </summary>
        /// <param name="formFile"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AgregarCodigosPostalesAsync([Required] IFormFile formFile)
        {
            string[] lines;

            var fechaInicial = DateTime.Now;
            var fechaFinal = DateTime.Now;
            StreamReader reader = new StreamReader(formFile.OpenReadStream(), System.Text.Encoding.Latin1);
            string text = reader.ReadToEnd();
            lines = text.Split("\n");
            await _repositorio.AgregarCodigosPostalesAsync(lines);
            fechaFinal = DateTime.Now;

            return Accepted(new { fechaInicial, fechaFinal, TotalDeRegistros = lines.Count(), Tiempo = (fechaInicial - fechaFinal).TotalSeconds });
        }

    }//end class
}
