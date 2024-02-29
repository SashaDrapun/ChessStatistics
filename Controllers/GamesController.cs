using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.GameServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    public class GamesController : Controller
    {
        public GamesController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost]
        public async Task<IActionResult> AddGame(GameModel gameModel)
        {
            await GameAdder.AddPassedGameAsync(gameModel);
            return RedirectToAction("AdminPanel", "Home");
        }
    }
}
