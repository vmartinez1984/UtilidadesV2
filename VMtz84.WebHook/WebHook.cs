using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace VMtz84.WebHook
{
    public class WebHook
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Body { get; set; }

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;

        public string Encodedkey { get; set; } = Guid.NewGuid().ToString();
    }
}
