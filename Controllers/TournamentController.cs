using ChessStatistics.BusinessLogic;
using ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Round;
using ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss;
using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.RequestsToParticipateInTournamentServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using ChessStatistics.ViewModels.TournamentPage;
using ChessStatistics.ViewModels.TournamentPage.RequestToParticipateInTournamentModels;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TournamentController : Controller
    {
        public TournamentController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost("AddTournamentParticipants")]
        public async Task<ActionResult> AddTournamentParticipants([FromForm] TournamentAddParticipantModel tournamentAddParticipantModel)
        {
            await TournamentParticipantsAdder.AddTournamentParticipantAsync(tournamentAddParticipantModel.IdPlayer, tournamentAddParticipantModel.IdTournament);
            
            return RedirectToAction("Tournament", "Home", new { idTournament = tournamentAddParticipantModel.IdTournament });
        }

        [HttpPost("AddingRequestToParticipateInTournament")]
        public async Task<ActionResult> AddingRequestToParticipateInTournament([FromForm] AddRequestToParticipateInTournamentModel addRequestToParticipateInTournamentModel)
        {
            await RequestToParticipateInTournamentAdder.AddTournamentRequestAsync(addRequestToParticipateInTournamentModel);
            
             return RedirectToAction("Tournament", "Home", new { idTournament = addRequestToParticipateInTournamentModel.IdTournament });
        }

        [HttpPost("ImproveOrDeclineRequestToParticipateInTournament")]
        public async Task<ActionResult> ImproveOrDeclineRequestToParticipateInTournament([FromForm] ImproveOrDeclineRequestToParticipateInTournamentModel improveOrDeclineRequestToParticipateInTournamentModel)
        {
            string userId = improveOrDeclineRequestToParticipateInTournamentModel.IdUser;
            int tournamentId = improveOrDeclineRequestToParticipateInTournamentModel.IdTournament;

            if (improveOrDeclineRequestToParticipateInTournamentModel.DeclineOrImprove == DeclineOrImprove.Improve)
            {
                Player player = PlayerSearcher.GetPlayerByIdUser(userId);
                await TournamentParticipantsAdder.AddTournamentParticipantAsync(player.IdPlayer, tournamentId);
            }

            await RequestToParticipateInTournamentDeleter.DeleteRequestToParticipateInTournament(userId, tournamentId);

            return RedirectToAction("Tournament", "Home", new { idTournament = improveOrDeclineRequestToParticipateInTournamentModel.IdTournament });
        }


        [HttpPost("DeleteTournamentParticipant")]
        public async Task<ActionResult> DeleteTournamentParticipant([FromForm]TournamentAddParticipantModel tournamentAddParticipantModel)
        {
            await TournamentParticipantsDeleter.DeleteTournamentParticipant(tournamentAddParticipantModel.IdTournament, tournamentAddParticipantModel.IdPlayer);

            return RedirectToAction("Tournament", "Home", new { idTournament = tournamentAddParticipantModel.IdTournament });
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
            return TournamentSearcher.GetRoundRobinResult(tournamentModel.IdTournament);
        }
    }
}
