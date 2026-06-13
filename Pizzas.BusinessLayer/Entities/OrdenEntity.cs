using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMtz84.Pizzas.Entities
{
    public class OrdenEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Encodedkey { get; internal set; }
        public DateTime FechaDeRegistro { get; internal set; }
        public string ClienteEncodedkey { get; internal set; }
        public List<PizzaEntity> Pizzas { get; internal set; }
        public List<ProductoEntity> Productos { get; internal set; }
        public string Estado { get; internal set; }
        public string MetodoDePago { get; internal set; }
    }
}
