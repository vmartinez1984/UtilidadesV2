using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMtz84.Asistencias.Entities
{
    public class UsuarioEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int Id { get; set; }

        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string Correo { get; set; }

        public string Contrasenia { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
    }
}
