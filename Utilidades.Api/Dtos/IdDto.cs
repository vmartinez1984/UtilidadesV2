using System.Text.Json.Serialization;

namespace Utilidades.Api.Dtos
{
    public class IdDto
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Id { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Mensaje { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
