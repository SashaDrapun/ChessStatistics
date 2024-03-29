﻿using ChessStatistics.BusinessLogic;
using ChessStatistics.BusinessLogic.GeneratingTournamentDraw;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    [Route("api/[controller]")]
    public class TournamentController : Controller
    {
        public TournamentController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost("AddTournamentParticipants")]
        public async Task<ActionResult<Player>> AddTournamentParticipants([FromBody] TournamentAddParticipantModel tournamentAddParticipantModel)
        {
            await TournamentParticipantsAdder.AddTournamentAsync(tournamentAddParticipantModel.IdPlayer, tournamentAddParticipantModel.IdTournament);
            ViewBag.PlayersNotParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentAddParticipantModel.IdTournament, false);
            ViewBag.PlayersParticipaningInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentAddParticipantModel.IdTournament, true);
            return Ok(PlayerSearcher.GetPlayerById(tournamentAddParticipantModel.IdPlayer));
        }

        [HttpPost("GenerateTournamentDraw")]
        public async Task<ActionResult<TournamentDrawModel>> GenerateTournamentDraw([FromBody] TournamentModel tournamentModel)
        {
            await GeneratingTournamentDraw.GenerateTournamentDrawAsync(tournamentModel.IdTournament);

            TournamentDrawModel result = TournamentSearcher.GetTournamentDraw(tournamentModel.IdTournament);
            return result;
        }

        [HttpPost("SetGameResult")]
        public async Task<ActionResult<GameModel>> SetGameResult([FromBody] GameModel gameModel)
        {
            await GameUpdater.UpdateGameAsync(gameModel);

            GameModel result = GameMapper.MapGame(GameSearcher.GetGame(gameModel.Id));
            return result;
        }
    }
}
