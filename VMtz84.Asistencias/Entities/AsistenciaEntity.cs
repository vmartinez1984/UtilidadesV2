using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMtz84.Asistencias.Entities
{
    public class AsistenciaEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public int Id { get; set; }

        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();

        public string UrlDeLaFoto { get; set; }

        public int UsuarioId { get; set; }

        public string Gps { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
    }
}
