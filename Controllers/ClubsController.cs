using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.ClubServices;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.UserServices;
using ChessStatistics.ViewModels.ClubPage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    public class ClubsController : Controller
    {
        public ClubsController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }


        [HttpPost]
        public async Task<IActionResult> Create(ClubModel clubModel, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await photo.CopyToAsync(memoryStream);
                    clubModel.Photo = memoryStream.ToArray();
                }
            }
            await ClubAdder.AddClubAsync(clubModel);
            return RedirectToAction("Clubs", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClubModel clubModel)
        {
            Club club = ClubSearcher.GetClubById(clubModel.Id);
            club.Description = clubModel.Description;
            club.Name = clubModel.Name;
            
            await ClubUpdater.UpdateClubAsync(club);
            return RedirectToAction("Clubs", "Home");
        }

        public async Task<IActionResult> Delete(int idClub)
        {
            await ClubDeleter.DeleteClub(idClub);
            return RedirectToAction("Clubs", "Home");
        }

    }
}
