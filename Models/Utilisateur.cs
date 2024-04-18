using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GestionHotelApi.Models
{
    public enum RoleUtilisateur
    {
        Client,
        Réceptionniste,
        PersonnelDeMénage
    }

    public class Utilisateur
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string Id { get; set; }

        public required string Nom { get; set; }

        public required string Email { get; set; }

        public required string MotDePasse { get; set; }

        public required RoleUtilisateur Role { get; set; }

        public Utilisateur()
        {
            Role = RoleUtilisateur.Client;
        }

    }

    public class Client : Utilisateur
    {
        public required string NuméroCarteBleue { get; set; }
    }

    public class Réceptionniste : Utilisateur
    {
    }

    public class PersonnelDeMénage : Utilisateur
    {
    }
}
