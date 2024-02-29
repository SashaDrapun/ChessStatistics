using ChessStatistics.BusinessLogic;
using ChessStatistics.BusinessLogic.GeneratingTournamentDraw;
using ChessStatistics.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    public class TournamentToursController : Controller
    {
        public TournamentToursController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost]
        public async Task<IActionResult> GenerateTournamentDraw(int idTournament)
        {
            await GeneratingTournamentDraw.GenerateTournamentDrawAsync(idTournament);

            return View("Tournament", idTournament);
        }
    }
}
