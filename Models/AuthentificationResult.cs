namespace GestionHotelApi.Models
{
    public class AuthentificationResult
    {
        public required string Token { get; set; }
        public required Utilisateur Utilisateur { get; set; }
    }

}
