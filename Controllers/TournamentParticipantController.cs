using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ChessStatistics.Controllers
{
    [Route("api/[controller]")]
    public class TournamentParticipantsController : Controller
    {
        public TournamentParticipantsController(ApplicationContext applicationContext)
        {
            Database.SetDB(applicationContext);
        }

        [HttpPost]
        public async Task<ActionResult<Player>> AddTournamentParticipants([FromBody] TournamentAddParticipantModel tournamentAddParticipantModel)
        {
            await TournamentParticipantsAdder.AddTournamentAsync(tournamentAddParticipantModel.IdPlayer, tournamentAddParticipantModel.IdTournament);
            await SetViewBag();
            ViewBag.PlayersNotParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentAddParticipantModel.IdTournament, false);
            ViewBag.PlayersParticipaningInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentAddParticipantModel.IdTournament, true);
            return Ok(PlayerSearcher.GetPlayerById(tournamentAddParticipantModel.IdPlayer));
        }

        [NonAction]
        public async Task<User> GetAutorizePlayer()
        {
            return await Database.db.Users.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
        }

        [NonAction]
        public async Task<bool> SetViewBag()
        {
            ViewBag.AutorizeUser = await GetAutorizePlayer();

            return true;
        }
    }
}
