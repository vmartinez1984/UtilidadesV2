using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;

namespace Utilidades.Repositorios.Entidades
{
    public class ApellidoNombre
    {
        [NotMapped]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int Id { get; set; }

        public string Dato { get; set; }

        public string Tipo { get; set; }
    }
}
