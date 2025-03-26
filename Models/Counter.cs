using MongoDB.Bson.Serialization.Attributes;

namespace BombaDeAgua_Api.Models
{
    public class Counter
    {
        [BsonId]
        public string Id { get; set; }
        public int SequenceValue { get; set; }
    }
}
