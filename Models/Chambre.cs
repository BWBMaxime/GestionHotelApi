using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GestionHotelApi.Models
{
    public class Chambre
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }
        public required string Type { get; set; }
        public required decimal Tarif { get; set; }
        public required int Capacite { get; set; }
        public required string Etat { get; set; }
       
    }
}
