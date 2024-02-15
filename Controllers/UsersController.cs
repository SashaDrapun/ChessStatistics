using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    public class UsersController : Controller
    {
        public UsersController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        public async Task<IActionResult> Delete(string idUser)
        {
            await UserDeleter.DeleteUser(idUser);
            return RedirectToAction("Users", "Home");
        }

        public async Task<IActionResult> AppointAdministrator(string idUser)
        {
            await UserUpdater.AppointAdministrator(idUser);
            return RedirectToAction("Users", "Home");
        }
    }
}
