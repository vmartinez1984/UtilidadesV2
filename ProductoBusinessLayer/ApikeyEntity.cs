using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductoBusinessLayer
{
    public class ApikeyEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }

        public string Apikey { get; set; }
        
        public string Nombre { get; set; }
        
        public string Correo { get; set; }
    
        public string Nota { get; set; }

        public bool EstaActivo { get; set; } = true;

        public DateTime FechaDeRegistro { get; set; } = DateTime.Now;
    }
}