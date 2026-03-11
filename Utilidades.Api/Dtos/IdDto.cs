using System.Text.Json.Serialization;

namespace Utilidades.Api.Dtos
{
    /// <summary>
    /// Represents a data transfer object containing an identifier, a message, and a timestamp.
    /// </summary>
    /// <remarks>This class is typically used to encapsulate simple response information, such as an operation
    /// result or status, including an optional identifier and message. The timestamp is set to the current date and
    /// time when the object is created.</remarks>
    public class IdDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Mensaje { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
