using GestionHotelApi.Models;
using GestionHotelApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestionHotelApi.Controllers
{
    [ApiController]
    [Route("api/chambre")]
    public class ChrambreController(ChambreService chambreService) : ControllerBase
    {
        private readonly ChambreService _chambreService = chambreService;

        [HttpGet]
        public async Task<List<Chambre>> GetAllChambre()
        {
           var listeChambre =  await _chambreService.GetAllChambreAsync();
            return listeChambre;
        }


        [HttpGet]
        [Route("/chambre-personnel-menage")]
        public async Task<List<Chambre>> GetAllPourMenageChambre()
        {
            var listeChambre = await _chambreService.GetAllChambrePourMenageChambreAsync();
            return listeChambre;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Chambre>> GetChambreById(string id)
        {
            var chambre = await _chambreService.GetChambreByIdAsync(id);

            if (chambre is null)
            {
                return NoContent();
            }

            return chambre;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Chambre newChambre)
        {
            await _chambreService.CreateAsync(newChambre);

            return CreatedAtAction(nameof(GetChambreById), new { id = newChambre.Id }, newChambre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Chambre updatedChambre)
        {
            var chambre = await _chambreService.GetChambreByIdAsync(id);

            if (chambre is null)
            {
                return NotFound();
            }

            updatedChambre.Id = chambre.Id;

            await _chambreService.UpdateAsync(id, updatedChambre);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var chambre = await _chambreService.GetChambreByIdAsync(id);

            if (chambre is null)
            {
                return NotFound();
            }

            await _chambreService.RemoveAsync(id);

            return NoContent();
        }
    }
}
