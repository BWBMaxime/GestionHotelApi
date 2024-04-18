using GestionHotelApi.Data;
using GestionHotelApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GestionHotelApi.Services
{
    public class UtilisateurService
    {
        private readonly IMongoCollection<Utilisateur> _utilisateurCollection;
        public UtilisateurService(
         IOptions<MongoDBSettings> mongoDBSettings)
        {
            var mongoClient = new MongoClient(
                mongoDBSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                mongoDBSettings.Value.DatabaseName);

            _utilisateurCollection = mongoDatabase.GetCollection<Utilisateur>(
                mongoDBSettings.Value.UtilisateurCollectionName);
        }

        internal async Task<Utilisateur> AuthenticateAsync(string email, string motDePasse)
        {
            var utilisateur = await _utilisateurCollection.Find(u => u.Email == email && u.MotDePasse == motDePasse).FirstOrDefaultAsync();
            return utilisateur;
        }

        internal async Task<List<Utilisateur>> GetAllUtilisateurAsync()
        {
            return await _utilisateurCollection.Find(utilisateur => true).ToListAsync();
        }

        internal async Task<Utilisateur> GetUtilisateurByIdAsync(string id)
        {
            return await _utilisateurCollection.Find(utilisateur => utilisateur.Id == id).FirstOrDefaultAsync();
        }
        internal async Task CreateAsync(Utilisateur newUtilisateur)
        {
            await _utilisateurCollection.InsertOneAsync(newUtilisateur);
        }

        internal async Task<ReplaceOneResult> UpdateAsync(string id, Utilisateur updatedUtilisateur)
        {
            return await _utilisateurCollection.ReplaceOneAsync(x => x.Id == id, updatedUtilisateur);
        }
        internal async Task RemoveAsync(string id)
        {
            await _utilisateurCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}
