using MongoDB.Driver;
using Microsoft.Extensions.Options;
using GestionHotelApi.Models;

namespace GestionHotelApi.Data
{
    public class MongoDBContext
    {
        public readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);

            // Crée la base de données si elle n'existe pas
            var databaseExists = client.ListDatabaseNames().ToList().Any(dbName => dbName == settings.Value.DatabaseName);
            if (!databaseExists)
            {
                client.GetDatabase(settings.Value.DatabaseName).CreateCollection("Hotel");
                client.GetDatabase(settings.Value.DatabaseName).DropCollection("Hotel");
            }
        }

        // Ajoutez ici les collections de votre base de données en tant que propriétés
        public IMongoCollection<Chambre> Chambre { get { return _database.GetCollection<Chambre>("Chambre"); } }
    }
}
