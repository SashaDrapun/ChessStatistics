using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    public class PlayersController : Controller
    {
        public PlayersController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer(PlayerModel playerModel)
        {
            await PlayerAdder.AddPlayerAsync(playerModel);
            return RedirectToAction("Players", "Home");
        }
    }
}
