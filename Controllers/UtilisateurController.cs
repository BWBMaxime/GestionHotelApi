using GestionHotelApi.Models;
using GestionHotelApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace GestionHotelApi.Controllers
{
    [ApiController]
    [Route("api/utilisateur")]
    public class UtilisateurController(UtilisateurService utilisateurService) : ControllerBase
    {
        private readonly UtilisateurService _utilisateurService = utilisateurService;

        [HttpPost("authentification")]
        public async Task<ActionResult<string>> Authenticate([FromBody] AuthenticationRequest request)
        {
            var utilisateur = await _utilisateurService.AuthenticateAsync(request.Email, request.MotDePasse);
            if (utilisateur == null)
            {
                return Unauthorized();
            }

            return "Utilisateur connecté";
        }

        [HttpGet]
        public async Task<List<Utilisateur>> GetAllUtilisateur()
        {
            var listeUtilisateur = await _utilisateurService.GetAllUtilisateurAsync();
            return listeUtilisateur;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Utilisateur>> GetUtilisateurById(string id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);

            if (utilisateur is null)
            {
                return NoContent();
            }

            return utilisateur;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Utilisateur newUtilisateur)
        {
            await _utilisateurService.CreateAsync(newUtilisateur);

            return CreatedAtAction(nameof(GetUtilisateurById), new { id = newUtilisateur.Id }, newUtilisateur);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Utilisateur updatedUtilisateur)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);

            if (utilisateur is null)
            {
                return NotFound();
            }

            updatedUtilisateur.Id = utilisateur.Id;

            await _utilisateurService.UpdateAsync(id, updatedUtilisateur);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var utilisateur = await _utilisateurService.GetUtilisateurByIdAsync(id);

            if (utilisateur is null)
            {
                return NotFound();
            }

            await _utilisateurService.RemoveAsync(id);

            return NoContent();
        }
    }
}
