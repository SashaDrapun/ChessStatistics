using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentParticipantsServices
{
    public static class TournamentParticipantsDeleter
    {
        public static async Task DeleteTournamentParticipants(int idTournament)
        {
            List<TournamentParticipants> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipantsByTournamentId(idTournament);

            foreach (var tournamentParticipant in tournamentParticipants) 
            {
                Database.db.Remove(tournamentParticipant);
            }
            
            await Database.db.SaveChangesAsync();
        }

        public static async Task DeleteTournamentParticipant(int idTournament, int idPlayer)
        {
            List<TournamentParticipants> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipants(idTournament, idPlayer);

            foreach (var  participant in tournamentParticipants)
            {
                Database.db.Remove(participant);
            }
            
            await Database.db.SaveChangesAsync();
        }
    }
}
