using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace ProductoBusinessLayer
{
    public class ProductoEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        [BsonElement("id")] 
        public int Id { get; set; }

        public string EncodedKey { get; set; } = Guid.NewGuid().ToString();

        [Required]
        public string Llave { get; set; }

        [BsonElement("valor01")]
        public string Valor01 { get; set; }

        [BsonElement("valor02")]
        public string Valor02 { get; set; }

        [BsonElement("valor03")]
        public string Valor03 { get; set; }

        [BsonElement("valor04")]
        public string Valor04 { get; set; }

        [BsonElement("estaActivo")]
        public bool EstaActivo { get; set; } = true;

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
    }
}
