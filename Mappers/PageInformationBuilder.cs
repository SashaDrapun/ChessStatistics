using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.RequestsToParticipateInTournamentServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using ChessStatistics.ViewModels.TournamentPage;
using ChessStatistics.ViewModels.TournamentsPage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Mappers
{
    public static class PageInformationBuilder
    {
        public static InformationOnPageTournamentsModel Tournaments()
        {
            var result = new InformationOnPageTournamentsModel();

            result.TournamentModels = TournamentMapper.
                MapTournamentsToTournamentsOnPageTournamentsModel(Database.db.Tournaments.ToList());
            result.Cities = TournamentSearcher.GetTournamentsCities();
            return result;
        }

        public static InformationOnPageTournamentModel Tournament(int idTournament, User autorizeUser)
        {
            var result = new InformationOnPageTournamentModel();

            result.PlayersParticipatingInTournament = PlayerMapper.MapListPlayersToListPlayerOnPlayersPage(PlayerSearcher.
               GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, true));
            result.PlayersNotParticipatingInTournament = PlayerMapper.MapListPlayersToListPlayerOnPlayersPage(PlayerSearcher.
                GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, false));
            result.RoundRobinTournamentResult = TournamentSearcher.GetRoundRobinResult(idTournament);
            result.TournamentDrawModel = TournamentSearcher.GetTournamentDraw(idTournament);
            result.UserInfo = new UserModel();

            if (autorizeUser != null)
            {
                result.UserInfo = UserMapper.MapUser(autorizeUser);
                TournamentRequests tournamentRequest = RequestToParticipateInTournamentSearcher.GetTournamentRequestByUserIdAndTournamentId(autorizeUser.Id, idTournament);
                result.IsUserSendRequestToParticipateInTournament = tournamentRequest == null ? false : true;
            }

            var requestsToParticipateInTournament = RequestToParticipateInTournamentSearcher.GetAllTournamentInTournamentRequests(idTournament);
            if (requestsToParticipateInTournament != null)
            {
                result.RequestsToParticipateInTournamentModels = RequestToParticipateInTournamentMapper.MapRequestsToParticipateInTournament(requestsToParticipateInTournament);
            }

            return result;
        }
    }
}
