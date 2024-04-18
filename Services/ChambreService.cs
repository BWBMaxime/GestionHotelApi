using Microsoft.Extensions.Options;
using MongoDB.Driver;
using GestionHotelApi.Models;
using GestionHotelApi.Data;

namespace GestionHotelApi.Services
{
    public class ChambreService
    {
        private readonly IMongoCollection<Chambre> _chambreCollection;

        public ChambreService(
            IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(
                mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDBSettings.Value.DatabaseName);

            _chambreCollection = mongoDatabase.GetCollection<Chambre>(
                mongoDBSettings.Value.ChambreCollectionName);
        }

        public async Task<List<Chambre>> GetAllChambreAsync()
        {
            return await _chambreCollection.Find(chambre => true ).ToListAsync();
        }
          
        public async Task<Chambre?> GetChambreByIdAsync(string id)
        {
            return await _chambreCollection.Find(chambre => chambre.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Chambre newChambre)
        {
            await _chambreCollection.InsertOneAsync(newChambre);
        }
        public async Task<ReplaceOneResult> UpdateAsync(string id, Chambre updatedChambre)
        {
            return await _chambreCollection.ReplaceOneAsync(x => x.Id == id, updatedChambre);
        }

        public async Task RemoveAsync(string id)
        {
            await _chambreCollection.DeleteOneAsync(x => x.Id == id);
        }
           
    }
}
