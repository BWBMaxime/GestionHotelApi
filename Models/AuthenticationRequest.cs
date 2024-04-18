namespace GestionHotelApi.Models
{
    public class AuthenticationRequest
    {
        public required string Email { get; set; }
        public required string MotDePasse { get; set; }
    }
}
