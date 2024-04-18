using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GestionHotelApi.Models
{
    public class Reservation
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public required Chambre[] Chambre { get; set; }

        public required string ClientId { get; set; }

        public required DateTime DateDébut { get; set; }

        public required DateTime DateFin { get; set; }

        public required bool StatutPaiement { get; set; }

        public required bool StatutAnnulation { get; set; }

        public required decimal MontantTotal { get; set; }
    }
}
