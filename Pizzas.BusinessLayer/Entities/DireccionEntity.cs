using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMtz84.Pizzas.Entities
{
    public class DireccionEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public int Id { get; set; }
        public string Encodedkey { get; set; }
        public string Calle { get; set; }
        public string Numero { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string CodigoPostal { get; set; }
        public string UsuarioEncodedkey { get; set; }
        public bool EsLaPrincipal { get; set; }
        public DateTime FechaDeRegistro { get; set; }
    }
}
