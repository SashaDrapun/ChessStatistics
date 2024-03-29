﻿using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChessStatistics.Models;
using ChessStatistics.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.LinkUserWithPlayerService;
using ChessStatistics.Mappers;

namespace ChessStatistics.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        public async Task<IActionResult> Index()
        {
            await SetViewBag();
            return View();
        }

        public async Task<IActionResult> Players()
        {
            await SetViewBag();
            return View(Database.db.Players.ToList());
        }

        public async Task<IActionResult> Users()
        {
            await SetViewBag();
            return View(Database.db.Users.ToList());
        }

        public async Task<IActionResult> Player(string idPlayer)
        {
            await SetViewBag();
            return View(UserSearcher.GetUserById(idPlayer));
        }

        public async Task<IActionResult> PersonalArea()
        {
            await SetViewBag();
            User autorizeUser = await GetAutorizeUser();
            ViewBag.IsUserConnectedToThePlayer = UserSearcher.IsUserConnectedToThePlayer(autorizeUser);
            ViewBag.IsUserRequestedLinkWithPlayer = LinkUserWithPlayerSearcher.IsUserRequestedLinkWithPlayer(autorizeUser);
            ViewBag.PlayersNotLinkedWithUser = PlayerSearcher.GetPlayersNotLinkedWithUser();
            return View(UserSearcher.GetUserById(autorizeUser.Id));
        }

        public async Task<IActionResult> Tournaments()
        {
            await SetViewBag();
            return View(Database.db.Tournaments.ToList());
        }

        public async Task<IActionResult> Tournament(int idTournament)
        {
            await SetViewBag();
            List<Player> PlayerParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, true);
            ViewBag.PlayersNotParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, false);
            ViewBag.PlayersParticipaningInTournament = PlayerParticipatingInTournament;
            ViewBag.CountPairs = PlayerParticipatingInTournament.Count % 2 == 0 ? PlayerParticipatingInTournament.Count / 2 : PlayerParticipatingInTournament.Count / 2 + 1;
            return View(TournamentSearcher.GetTournamentModelById(idTournament));
        }

        public async Task<IActionResult> Games()
        {
            await SetViewBag();
            return View(GameMapper.MapGames(GameSearcher.GetAllGames()));
        }

        public async Task<IActionResult> AdminPanel()
        {
            ViewBag.LinksUserWithPlayers = LinkUserWithPlayerSearcher.GetLinksUserWithPlayers();
            await SetViewBag();
            return View(Database.db.Players.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public async Task<User> GetAutorizeUser()
        {
            return await Database.db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        }

        [NonAction]
        public async Task<bool> SetViewBag()
        {
            User autorizeUser = await GetAutorizeUser();
            ViewBag.AutorizeUser = autorizeUser;
            ViewBag.IsAdmin = autorizeUser != null && autorizeUser.IsAdmin;

            return true;
        }
    }
}
