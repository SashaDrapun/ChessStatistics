using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using ChessStatistics.Services.ToursServices;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentServices
{
    public static class TournamentDeleter
    {
        public static async Task DeleteTournament(int idTournament)
        {
            await TournamentParticipantsDeleter.DeleteTournamentParticipants(idTournament);
            await TourDeleter.DeleteTours(idTournament);

            Tournament tournament = TournamentSearcher.GetTournamentById(idTournament);
            Database.db.Remove(tournament);
            await Database.db.SaveChangesAsync();
        }
    }
}
