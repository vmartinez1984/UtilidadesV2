using MongoDB.Bson.Serialization.Attributes;

public class ContadorEntity
{
    [BsonId]
    public string Id { get; set; } = string.Empty;

    public int Secuencia { get; set; }
}