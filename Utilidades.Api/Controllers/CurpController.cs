using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios.Curp;
using Utilidades.Servicios.Curp.Enums;

namespace Utilidades.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurpController : ControllerBase
    {
        /// <summary>
        /// Genera el curp
        /// </summary>
        /// <returns></returns>
        /// <response code="200"></response>        
        /// <param name="solicitud">Sexo: m / h. m para mujer, h para hombre.
        /// Para el estado: Aguascalientes, Baja_California, Baja_California_Sur, Campeche, Coahuila, Colima, Chiapas, Chihuahua, Distrito_Federal, Durango, Guanajuato, Guerrero, Hidalgo, Jalisco, Mexico, Michoacan, Morelos, Nayarit, Nuevo_Leon, Oaxaca, Puebla, Queretaro, Quintana_Roo, San_Luis_Potosi, Sinaloa, Sonora, Tabasco, Tamaulipas, Tlaxcala, Veracruz, Yucatan, Zacatecas, Extranjero
        /// </param>
        [ProducesResponseType(typeof(CurpDto), 200)]
        [Produces("application/json")]
        [HttpPost]
        public IActionResult GenerarCurp(SolicitudDeCurp solicitud)
        {
            string curp;

            // Convertir el texto de Estado al enum correspondiente (ignora mayúsculas/minúsculas)
            if (!Enum.TryParse<Estado>(solicitud.Estado, true, out var estadoEnum))
            {
                return BadRequest($"El campo Estado debe ser una clave válida del catálogo Estado.");
            }

            curp = CurpServicio.Generar(
                solicitud.Nombres,
                solicitud.PrimerApellido,
                solicitud.SegundoApellido,
                solicitud.Sexo.ToUpperInvariant() == "H" ? Sexo.Hombre : Sexo.Mujer,
                solicitud.FechaDeNacimiento,
                estadoEnum
            );

            return Created("", new CurpDto(curp));
        }


    }

    public class CurpDto
    {
        public CurpDto(string curp)
        {
            this.Curp = curp;
            this.Fecha = DateTime.Now;
        }

        public string Curp { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }

    public class SolicitudDeCurp
    {
        [Required]
        [MaxLength(50)]
        public string Nombres { get; set; }

        [Required]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }
                
        [MaxLength(50)]
        public string SegundoApellido { get; set; }

        [Required]
        public DateTime FechaDeNacimiento { get; set; }

        /// <summary>
        /// H, M
        /// </summary>
        [Required]
        [RegularExpression("^[HhMm]$", ErrorMessage = "El campo Sexo solo acepta 'H' para hombre o 'M' para mujer.")]
        [MinLength(1)]
        [MaxLength(1)]
        public string Sexo { get; set; }

        /// <summary>
        /// Aguascalientes, Baja_California, Baja_California_Sur, Campeche, Coahuila, Colima, Chiapas, Chihuahua, Distrito_Federal, Durango, Guanajuato, Guerrero, Hidalgo, Jalisco, Mexico, Michoacan, Morelos, Nayarit, Nuevo_Leon, Oaxaca, Puebla, Queretaro, Quintana_Roo, San_Luis_Potosi, Sinaloa, Sonora, Tabasco, Tamaulipas, Tlaxcala, Veracruz, Yucatan, Zacatecas, Extranjero
        /// </summary>
        [EstadoValido]
        [Required]
        public string Estado { get; set; }

    }

    public class EstadoValidoAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string estadoStr && !string.IsNullOrWhiteSpace(estadoStr))
            {
                var nombresValidos = System.Enum.GetNames(typeof(Estado));
                if (nombresValidos.Any(e => e.Equals(estadoStr, System.StringComparison.OrdinalIgnoreCase)))
                {
                    return ValidationResult.Success;
                }
                return new ValidationResult($"El campo Estado debe ser una clave válida del catálogo Estado: {string.Join(", ", nombresValidos)}.");
            }
            return new ValidationResult("El campo Estado es obligatorio.");
        }
    }
}
