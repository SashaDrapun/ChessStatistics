using System.Threading.Tasks;
using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services;
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

        [HttpPost]
        public async Task<IActionResult> EditTournament(TournamentModel tournamentModel)
        {
            await TournamentUpdater.UpdateTournamentAsync(tournamentModel);
            return RedirectToAction("Tournaments", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteTournament(int idTournament)
        {
            await TournamentDeleter.DeleteTournament(idTournament);
            return RedirectToAction("Tournaments", "Home");
        }


        [NonAction]
        public async Task<User> GetAutorizePlayer()
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
