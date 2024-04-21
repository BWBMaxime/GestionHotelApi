using GestionHotelApi.Data;
using GestionHotelApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestionHotelApi.Services
{
    public class ReservationService
    {
        private readonly IMongoCollection<Reservation> _reservationCollection;
        private readonly ChambreService _chambreService;

        public ReservationService(IOptions<MongoDBSettings> mongoDBSettings, ChambreService chambreService)
        {
            var mongoClient = new MongoClient(mongoDBSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _reservationCollection = mongoDatabase.GetCollection<Reservation>(mongoDBSettings.Value.ReservationCollectionName);
            _chambreService = chambreService;
        }

        internal async Task<List<Chambre>> GetChambreNonReserveAsync(DateTime from, DateTime to)
        {
            var filter = Builders<Reservation>.Filter.And(
                Builders<Reservation>.Filter.Or(
                    Builders<Reservation>.Filter.Gte(r => r.DateDébut, from),
                    Builders<Reservation>.Filter.Lte(r => r.DateFin, to)
                ),
                Builders<Reservation>.Filter.Or(
                    Builders<Reservation>.Filter.Gte(r => r.DateDébut, from),
                    Builders<Reservation>.Filter.Lte(r => r.DateFin, to)
                )
            );

            var reservations = await _reservationCollection.Find(filter).ToListAsync();

            var chambreReserveeIds = new HashSet<string>();
            foreach (var reservation in reservations)
            {
                foreach (var chambre in reservation.Chambre)
                {
                    chambreReserveeIds.Add(chambre.Id);
                }
            }

            var allChambres = await _chambreService.GetAllChambreAsync();

            var chambresNonReservees = allChambres.Where(chambre => !chambreReserveeIds.Contains(chambre.Id)).ToList();

            return chambresNonReservees;
        }


        public async Task<Reservation> CreateReservationAsync(Reservation reservation,int cardNumbers)
        {
            foreach (var chambreId in reservation.Chambre.Select(c => c.Id))
            {
                bool chambreDisponible = await IsChambreAvailable(chambreId, reservation.DateDébut, reservation.DateFin);
                if (!chambreDisponible)
                {
                    throw new Exception($"La chambre avec l'ID {chambreId} n'est pas disponible pour les dates spécifiées.");
                }
            }

            decimal prixTotal = CalculateTotalPrice(reservation);

            // Traitement de paiement (service factice)
            //bool paiementEffectue = await _paymentService.ProcessPaymentAsync(cardNumbers, prixTotal);
            //if (!paiementEffectue)
            //{
            //    throw new Exception("Le paiement n'a pas pu être effectué.");
            //}

            reservation.MontantTotal = prixTotal;
            await _reservationCollection.InsertOneAsync(reservation);
            return reservation;
        }

        private async Task<bool> IsChambreAvailable(string chambreId, DateTime dateDebut, DateTime dateFin)
        {
            var reservations = await _reservationCollection.Find(r =>
                r.Chambre.Any(c => c.Id == chambreId) &&
                ((r.DateDébut >= dateDebut && r.DateDébut < dateFin) || (r.DateFin > dateDebut && r.DateFin <= dateFin))
            ).ToListAsync();

            return reservations.Count == 0;
        }

        private decimal CalculateTotalPrice(Reservation reservation)
        {
            decimal prixTotal = 0;

            foreach (var chambre in reservation.Chambre)
            {
                prixTotal += chambre.Tarif;
            }

            int nombreNuits = (int)(reservation.DateFin - reservation.DateDébut).TotalDays;

            prixTotal *= nombreNuits;

            return prixTotal;
        }
        public async Task<Reservation> GetReservationByIdAsync(string id)
        {
            return await _reservationCollection.Find(r => r.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Reservation>> GetAllReservationsAsync()
        {
            return await _reservationCollection.Find(_ => true).ToListAsync();
        }

        public async Task UpdateReservationAsync(Reservation reservation)
        {
            await _reservationCollection.ReplaceOneAsync(r => r.Id == reservation.Id, reservation);
        }

        public async Task DeleteReservationAsync(string id)
        {
            await _reservationCollection.DeleteOneAsync(r => r.Id == id);
        }
    }
}
