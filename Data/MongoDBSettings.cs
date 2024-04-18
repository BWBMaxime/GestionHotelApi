namespace GestionHotelApi.Data
{
    public class MongoDBSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ChambreCollectionName { get; set; } = null!;
        public string UtilisateurCollectionName { get; set; } = null!;
        public string ReservationCollectionName { get; set; } = null!;
    }
}