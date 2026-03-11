using MongoDB.Bson.Serialization.Attributes;

namespace Contador.BusinessLayer
{
    public class Contador
    {
        [BsonId]
        public int Id { get; set; }

        public string Key { get; set; }

        public int ValorActual { get; set; }

        public int ValorMin { get; set; }

        public int ValorMax { get; set; }
    }
}
