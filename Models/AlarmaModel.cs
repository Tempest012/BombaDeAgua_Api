using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BombaDeAgua_Api.Models
{
    public class AlarmaModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }

        public int IdUsuario { get; set; }

        // Solo hora (ej: 08:30:00)
        [BsonRepresentation(BsonType.String)]
        public TimeSpan Hora { get; set; }

        public bool Estado { get; set; }

    }
}
