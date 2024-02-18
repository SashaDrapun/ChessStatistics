using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.LinkUserWithPlayerService;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    public class LinkUserWithPlayerController : Controller
    {
        public LinkUserWithPlayerController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost]
        public async Task<IActionResult> LinkUser(LinkUserWithPlayerModel linkUserWithPlayerModel)
        {
            await LinkUserWithPlayerAdder.AddLink(linkUserWithPlayerModel);

            return RedirectToAction("PersonalArea", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> LinkUserConfirmOrDecline(int idLink, ConfirmOrDecline confirmOrDecline)
        {
            if (confirmOrDecline == ConfirmOrDecline.Confirm) 
            {
                await UserUpdater.SetPlayer(idLink);
            }

            await LinkUserWithPlayerDeleter.DeleteLinkUser(idLink);

            return RedirectToAction("AdminPanel", "Home");
        }

        public enum ConfirmOrDecline
        {
            Confirm,
            Decline,
        }
    }
}
