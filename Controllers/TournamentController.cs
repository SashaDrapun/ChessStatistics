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
        public async Task<ActionResult> GenerateTournamentDraw([FromForm] int idTournament)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(idTournament);

            if (tournament.TournamentType == TournamentType.Round)
            {
                if (tournament.CountTours == 2)
                {
                    await TournamentUpdater.SetCountToursAsync(idTournament);
                    await GeneratingRoundTournamentDraw.GenerateTournamentDrawAsync(idTournament);
                }
            }

            if (tournament.TournamentType == TournamentType.Swiss)
            {
                await GeneratingSwissTournamentDraw.AddNextTourAsync(idTournament);
            }

            return RedirectToAction("Tournament", "Home", new { idTournament });

        }

        [HttpPost("SetGameResult")]
        public async Task<ActionResult> SetGameResult([FromForm] SetGameResultModel setGameResultModel)
        {
            await GameUpdater.UpdateGameAsync(setGameResultModel);
            
            return RedirectToAction("Tournament", "Home", 
                new { idTournament = setGameResultModel.IdTournament,
                    tabPosition = setGameResultModel.TabPosition,
                    tourPosition = setGameResultModel.TourPosition,
                });
        }

        [HttpPost("GetTournamentResult")]
        public ActionResult<RoundRobinTournamentResult> GetTournamentResult([FromBody] TournamentModel tournamentModel)
        {
            return TournamentSearcher.GetRoundRobinResult(tournamentModel.IdTournament);
        }
    }
}
