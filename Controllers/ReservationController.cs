using GestionHotelApi.Models;
using GestionHotelApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionHotelApi.Controllers
{
    public class ReservationController(ReservationService reservationService) : ControllerBase
    {
        public readonly ReservationService _reservationService1 = reservationService;

        [HttpGet]
        public async Task<List<Chambre>> GetChambreNonReserve(DateTime from, DateTime to)
        {
            var listeChambre = await _reservationService1.GetChambreNonReserveAsync(from, to);
            return listeChambre;
        }

    }
}
