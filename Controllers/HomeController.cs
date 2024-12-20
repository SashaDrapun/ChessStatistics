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
using ChessStatistics.ViewModels;
using ChessStatistics.BusinessLogic.ParseInformationFromLichess;
using ChessStatistics.Services.RequestsToParticipateInTournamentServices;
using ChessStatistics.Services.ClubServices;
using ChessStatistics.ViewModels.MainPage;
using ChessStatistics.ViewModels.TournamentPage;


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
            var html = await BlogPost.FetchPageAsync("https://lichess.org/@/Lichess/blog");
            var blogPosts = BlogPost.ParseBlogPosts(html);

            var viewModel = new NewsViewModel
            {
                BlogPosts = blogPosts
            };

            ViewData["NewsViewModel"] = viewModel;

            return View();
        }

        public async Task<IActionResult> Clubs()
        {
            await SetViewBag();
            
            return View(ClubMapper.MapClubs(ClubSearcher.GetClubs()));
        }

        public async Task<IActionResult> Donation()
        {
            await SetViewBag();
            
            return View();
        }

        public async Task<IActionResult> Players()
        {
            await SetViewBag();
            var players = Database.db.Players.ToList();
            return View(PlayerMapper.MapListPlayersToListPlayerOnPlayersPage(players));
        }

        public async Task<IActionResult> Users()
        {
            await SetViewBag();
            return View(Database.db.Users.ToList());
        }

        public async Task<IActionResult> Player(int idPlayer)
        {
            await SetViewBag();
            return View(PlayerMapper.MapPlayerToPlayerOnPagePlayerModel(PlayerSearcher.GetPlayerById(idPlayer)));
        }

        public async Task<IActionResult> PersonalArea(int idPlayer)
        {
            await SetViewBag();
            PlayerOnPlayerPageModel playerOnPlayerPageModel = new PlayerOnPlayerPageModel();

            if (idPlayer == 0)
            {
                playerOnPlayerPageModel.IsPersonalArea = true;
                User autorizeUser = await GetAutorizeUser();

                if (UserSearcher.IsUserConnectedToThePlayer(autorizeUser))
                {
                    playerOnPlayerPageModel = PlayerMapper.MapPlayerToPlayerOnPagePlayerModel(PlayerSearcher.GetPlayerById(autorizeUser.IdPlayer));
                    playerOnPlayerPageModel.IsPersonalArea = true;
                }
                else
                {
                    playerOnPlayerPageModel = PlayerMapper.MapPlayerToPlayerOnPagePlayerModel(autorizeUser.Id);
                    playerOnPlayerPageModel.IsPersonalArea = true;
                }
                 
            }
            else 
            {
                playerOnPlayerPageModel = PlayerMapper.MapPlayerToPlayerOnPagePlayerModel(PlayerSearcher.GetPlayerById(idPlayer));
                playerOnPlayerPageModel.IsPersonalArea = false;
            }
            
            ViewBag.PlayersNotLinkedWithUser = PlayerSearcher.GetPlayersNotLinkedWithUser();
            return View(playerOnPlayerPageModel);
        }

        public async Task<IActionResult> Tournaments()
        {
            await SetViewBag();
            
            return View(PageInformationBuilder.Tournaments());
        }

        public async Task<IActionResult> Tournament(int idTournament, int tabPosition = 1, int tourPosition = 1)
        {
            await SetViewBag();
            User user = await GetAutorizeUser();
            ViewData["CurrentPage"] = "Tournament";
            return View(PageInformationBuilder.Tournament(idTournament, user, new UserPosition(tabPosition, tourPosition)));
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
