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
        public string EncodedKey { get; set; }

        [BsonElement("tags")]
        public string Tags { get; set; }

        [BsonElement("valor01")]
        public string Valor01 { get; set; }

        [BsonElement("valor02")]
        public string Valor02 { get; set; }

        [BsonElement("valor03")]
        public string Valor03 { get; set; }

        [BsonElement("valor04")]
        public string Valor04 { get; set; }

        [BsonElement("fechaDeRegistro")]
        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        [BsonElement("estaActivo")]
        public bool EstaActivo { get; set; } = true;

        [BsonElement("__v")]
        public int Version { get; set; }
    }
}
