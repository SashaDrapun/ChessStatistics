using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
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
        public async Task<IActionResult> CreatePlayer(PlayerModel playerModel, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    playerModel.Photo = memoryStream.ToArray();
                }
            }
            await PlayerAdder.AddPlayerAsync(playerModel);
            return RedirectToAction("Players", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> EditPlayer(PlayerModel playerModel)
        {
            Player player = PlayerSearcher.GetPlayerById(playerModel.IdPlayer);
            player.Rank = playerModel.Rank;
            player.FIO = playerModel.FIO;
            await PlayerUpdater.UpdatePlayerAsync(player);
            return RedirectToAction("Players", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePlayer(int idPlayer)
        {
            await PlayerDeleter.DeletePlayer(idPlayer);
            return RedirectToAction("Players", "Home");
        }
    }
}
