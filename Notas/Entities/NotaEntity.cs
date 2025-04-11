using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Notas.Entities
{
    public class NotaEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("contenido")]
        public string Contenido { get; set; }

        [BsonElement("tags")]
        public string Tags { get; set; }

        [BsonElement("estado")]
        public string Estado { get; set; }

        [BsonElement("fechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; }

        [BsonElement("fechaInicio")]
        public DateTime? FechaInicio { get; set; }

        [BsonElement("fechaFin")]
        public DateTime? FechaFin { get; set; }

        [BsonElement("__v")]
        public int Version { get; set; }
    }
}
