using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using System.Threading.Tasks;

namespace ChessStatistics.Services.RequestsToParticipateInTournamentServices
{
    public static class RequestToParticipateInTournamentDeleter
    {
        public static async Task DeleteRequestToParticipateInTournament(string userId, int idTournament)
        {
            TournamentRequests tournamentRequests 
                = RequestToParticipateInTournamentSearcher.GetTournamentRequestByUserIdAndTournamentId(userId, idTournament);
            Database.db.Remove(tournamentRequests);
            await Database.db.SaveChangesAsync();
        }
    }
}
