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

            var allChambres = await _chambreService.GetAsync();

            var chambresNonReservees = allChambres.Where(chambre => !chambreReserveeIds.Contains(chambre.Id)).ToList();

            return chambresNonReservees;
        }

    }
}
