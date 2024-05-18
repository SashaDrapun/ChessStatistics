using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using ChessStatistics.Models;
using ChessStatistics.BusinessLogic;
using ChessStatistics.ViewModels;
using ChessStatistics.Services;
using ChessStatistics.Services.PlayerServices;
using Microsoft.EntityFrameworkCore;

namespace ChessStatistics.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> playerManager;

        public AccountController(ApplicationContext applicationContext, SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            Database.SetDB(applicationContext);
            this.signInManager = signInManager;
            this.playerManager = userManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            User playerFromDatabaseByEmail = UserSearcher.GetUserByEmail(model.Email);
            bool isAllValid = AreThereAnyProblems(model, playerFromDatabaseByEmail);

            if (isAllValid)
            {
                User user = new(model.Email)
                {
                    UserName = model.Email,
                    FIO = model.FIO,
                    Birthday = model.Birthday,
                    DateRegistration = DateTime.Now,
                    DateLastLogin = DateTime.Now,
                };
                if (!Database.db.Users.Any())
                {
                    user.IsAdmin = true;
                }
                var result = await playerManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        private bool AreThereAnyProblems(RegisterModel model, User playerFromDatabaseByEmail)
        {
            bool isAllValid = true;

            if (string.IsNullOrEmpty(model.Email))
            {
                ViewBag.EmailMessage = "Заполните поле";
                isAllValid = false;
            }

            if (string.IsNullOrEmpty(model.FIO))
            {
                ViewBag.FIOMessage = "Заполните поле";
                isAllValid = false;
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.PasswordMessage = "Заполните поле";
                isAllValid = false;
            }

            if (string.IsNullOrEmpty(model.ConfirmPassword))
            {
                ViewBag.ConfirmPasswordMessage = "Заполните поле";
                isAllValid = false;
            }

            if (model.Password != model.ConfirmPassword)
            {
                ViewBag.ConfirmPasswordMessage = "Пароли не совпадают";
                isAllValid = false;
            }

            if (playerFromDatabaseByEmail != null)
            {
                ViewBag.EmailMessage = "Пользователь с такой почтой уже существует";
                isAllValid = false;
            }

            return isAllValid;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            User playerFromDatabase = UserSearcher.GetUserByEmail(model.Email);
            bool isAllValid = true;

            if (string.IsNullOrEmpty(model.Password))
            {
                ViewBag.PasswordMessage = "Заполните поле";
                isAllValid = false;
            }

            if (playerFromDatabase == null)
            {
                ViewBag.EmailMessage = "Игрока с такой почтой не существует";
                isAllValid = false;
            }

            if (isAllValid)
            {
                playerFromDatabase.DateLastLogin = DateTime.Now;

                await UserUpdater.UpdateUserAsync(playerFromDatabase);
                var result = await signInManager.PasswordSignInAsync(playerFromDatabase.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.PasswordMessage = "Неправильный пароль";
                }
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
