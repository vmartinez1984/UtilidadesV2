using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Utilidades.Servicios.Curp;
using Utilidades.Servicios.Curp.Enums;

namespace Utilidades.Api.Controllers
{
    /// <summary>
    /// controller para el curp
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
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
        /// <summary>
        /// Gets or sets the first names of the person.
        /// </summary>
        /// <remarks>The value is required and cannot exceed 50 characters in length.</remarks>
        [Required]
        [MaxLength(50)]
        public string Nombres { get; set; }

        /// <summary>
        /// Gets or sets the first surname of the person.
        /// </summary>
        /// <remarks>The value is required and must not exceed 50 characters in length.</remarks>
        [Required]
        [MaxLength(50)]
        public string PrimerApellido { get; set; }
                
        /// <summary>
        /// Gets or sets the second surname of the person.
        /// </summary>
        [MaxLength(50)]
        public string SegundoApellido { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
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

    /// <summary>
    /// Provides a validation attribute that ensures a string value corresponds to a valid key in the Estado
    /// enumeration.
    /// </summary>
    /// <remarks>Use this attribute to validate that a property or field contains a valid Estado value. The
    /// validation is case-insensitive and requires the value to be non-empty. If the value is not a valid Estado key, a
    /// validation error is returned listing all valid keys.</remarks>
    public class EstadoValidoAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates that the provided value is a non-empty string corresponding to a valid key in the Estado
        /// enumeration.
        /// </summary>
        /// <remarks>The validation is case-insensitive and requires the value to match one of the defined
        /// Estado enumeration names. If the value is null, empty, or whitespace, validation fails and an error message
        /// is returned.</remarks>
        /// <param name="value">The value to validate. Must be a non-empty string representing a key in the Estado enumeration.</param>
        /// <param name="validationContext">The context information about the validation operation, including the object instance and metadata.</param>
        /// <returns>A ValidationResult indicating success if the value is a valid Estado key; otherwise, a ValidationResult with
        /// an error message describing the validation failure.</returns>
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
