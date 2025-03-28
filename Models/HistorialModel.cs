using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BombaDeAgua_Api.Models
{
    public class HistorialModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public int Porcentaje { get; set; }
        
    }
}
