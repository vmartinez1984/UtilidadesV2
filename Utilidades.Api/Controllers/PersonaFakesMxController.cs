using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Repositorios.Entidades;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// Registro de nombres y apellidos para generar una persona Fake de México
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PersonaFakesMxController : ControllerBase
    {
        //private readonly RepositorioDeNombresYApellidosMx _repositorio;

        //public PersonaFakesMxController(RepositorioDeNombresYApellidosMx repositorio)
        //{
        //    _repositorio = repositorio;
        //}

        //[HttpPost("Nombres/Hombre")]
        //public async Task<IActionResult> AgregarNombres(List<string> nombres)
        //{
        //    List<ApellidoNombre> nombresExistentes;
        //    List<ApellidoNombre> noExistentes = new List<ApellidoNombre>();
        //    List<ApellidoNombre> lista = new List<ApellidoNombre>();

        //    nombres.ForEach(item =>
        //    {
        //        lista.Add(new ApellidoNombre
        //        {
        //            Tipo = "Nombre Hombre",
        //            Dato = item
        //        });
        //    });
        //    nombresExistentes = await _repositorio.ObtenerNombresAsync();
        //    foreach (var nombre in lista)
        //    {
        //        var item = nombresExistentes.FirstOrDefault(item => CompararCadenas(item.Dato, nombre.Dato));
        //        if (item == null)
        //            noExistentes.Add(nombre);
        //    }
        //    if (noExistentes.Any())
        //    {
        //        await _repositorio.AgregarNombresAsync(noExistentes);
        //        return Created(string.Empty, noExistentes);
        //    }
        //    else
        //    {
        //        return Ok(new { Mensaje = "Registrados previamente", nombres });
        //    }
        //}

        //[HttpPost("Nombres/Mujer")]
        //public async Task<IActionResult> AgregarNombresDeMuejer(List<string> nombres)
        //{
        //    List<ApellidoNombre> nombresExistentes;
        //    List<ApellidoNombre> noExistentes = new List<ApellidoNombre>();
        //    List<ApellidoNombre> lista = new List<ApellidoNombre>();

        //    nombres.ForEach(item =>
        //    {
        //        lista.Add(new ApellidoNombre
        //        {
        //            Tipo = "Nombre Mujer",
        //            Dato = item
        //        });
        //    });
        //    nombresExistentes = await _repositorio.ObtenerNombresAsync();
        //    foreach (var nombre in lista)
        //    {
        //        var item = nombresExistentes.FirstOrDefault(item => CompararCadenas(item.Dato, nombre.Dato));
        //        if (item == null)
        //            noExistentes.Add(nombre);
        //    }
        //    if (noExistentes.Any())
        //    {
        //        await _repositorio.AgregarNombresAsync(noExistentes);
        //        return Created(string.Empty, noExistentes);
        //    }
        //    else
        //    {
        //        return Ok(new { Mensaje = "Registrados previamente", nombres });
        //    }
        //}

        ///// <summary>
        ///// Compara dos cadenas ignorando mayusculas, minusculsa y acentos
        ///// </summary>
        ///// <param name="cadena1"></param>
        ///// <param name="cadena2"></param>
        ///// <returns></returns>
        //private bool CompararCadenas(string cadena1, string cadena2)
        //{
        //    var comparador = StringComparer.Create(CultureInfo.InvariantCulture, CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreCase);
        //    return comparador.Equals(cadena1, cadena2);
        //}

        //[HttpPost("Apellidos")]
        //public async Task<IActionResult> AgregarApellidos(List<string> apellidos)
        //{
        //    List<ApellidoNombre> existentes;
        //    List<ApellidoNombre> noExistentes = new List<ApellidoNombre>();
        //    List<ApellidoNombre> lista = new List<ApellidoNombre>();

        //    apellidos.ForEach(item =>
        //    {
        //        lista.Add(new ApellidoNombre
        //        {
        //            Tipo = "Apellido",
        //            Dato = item
        //        });
        //    });
        //    existentes = await _repositorio.ObtenerApellidosAsync();
        //    foreach (var nombre in lista)
        //    {
        //        var item = existentes.FirstOrDefault(item => CompararCadenas(item.Dato, nombre.Dato));
        //        if (item == null)
        //            noExistentes.Add(nombre);
        //    }
        //    if (noExistentes.Any())
        //    {
        //        await _repositorio.AgregarApellidosAsync(noExistentes);
        //        return Created(string.Empty, noExistentes);
        //    }
        //    else
        //    {
        //        return Ok(new { Mensaje = "Registrados previamente", apellidos });
        //    }
        //}
    }
}