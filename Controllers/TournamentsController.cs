using System.Threading.Tasks;
using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services;
using ChessStatistics.Services.TournamentParticipantsServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChessStatistics.Controllers
{
    public class TournamentsController : Controller
    {

        public TournamentsController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTournament(TournamentModel tournamentModel)
        {
            await TournamentAdder.AddTournamentAsync(tournamentModel);
            return RedirectToAction("Tournaments", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> AddTournamentParticipants(int id)
        {
            await SetViewBag();
            ViewBag.PlayersNotParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(id, false);
            ViewBag.PlayersParticipaningInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(id, true);
            return View(TournamentSearcher.GetTournamentById(id));
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddTournamentParticipants(int tournamentId, string playerId)
        {
            await TournamentParticipantsAdder.AddTournamentAsync(playerId, tournamentId);
            await SetViewBag();
            ViewBag.PlayersNotParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentId, false);
            ViewBag.PlayersParticipaningInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentId, true);
            return View(TournamentSearcher.GetTournamentById(tournamentId));
        }

        [NonAction]
        public async Task<Player> GetAutorizePlayer()
        {
            return await Database.db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        }

        [NonAction]
        public async Task<bool> SetViewBag()
        {
            ViewBag.AutorizeUser = await GetAutorizePlayer();

            return true;
        }
    }
}
