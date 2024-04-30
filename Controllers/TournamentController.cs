using ChessStatistics.BusinessLogic;
using ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Round;
using ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss;
using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
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
            Player player = PlayerSearcher.GetPlayerById(tournamentAddParticipantModel.IdPlayer);
            return Ok(PlayerMapper.MapPlayer(player, tournamentAddParticipantModel.IdTournament));
        }

        [HttpPost("GenerateTournamentDraw")]
        public async Task<ActionResult<TournamentDrawModel>> GenerateTournamentDraw([FromBody] TournamentModel tournamentModel)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(tournamentModel.IdTournament);

            if (tournament.TournamentType == TournamentType.Round)
            {
                if (tournament.CountTours == 2)
                {
                    await TournamentUpdater.SetCountToursAsync(tournamentModel.IdTournament);
                    await GeneratingRoundTournamentDraw.GenerateTournamentDrawAsync(tournamentModel.IdTournament);
                }
            }

            if (tournament.TournamentType == TournamentType.Swiss)
            {
                await GeneratingSwissTournamentDraw.AddNextTourAsync(tournamentModel.IdTournament);
            }

            TournamentDrawModel result = TournamentSearcher.GetTournamentDraw(tournamentModel.IdTournament);
            return result;
        }

        [HttpPost("SetGameResult")]
        public async Task<ActionResult<GameModel>> SetGameResult([FromBody] GameModel gameModel)
        {
            await GameUpdater.UpdateGameAsync(gameModel);

            GameModel result = GameMapper.MapGame(GameSearcher.GetGame(gameModel.IdGame));
            return result;
        }

        [HttpPost("GetTournamentResult")]
        public ActionResult<RoundRobinTournamentResult> GetTournamentResult([FromBody] TournamentModel tournamentModel)
        {
            return TournamentSearcher.GetRoundRobitResult(tournamentModel.IdTournament);
        }
    }
}
