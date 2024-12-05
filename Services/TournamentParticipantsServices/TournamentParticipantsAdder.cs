using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentParticipantsServices
{
    public static class TournamentParticipantsAdder
    {
        public static async Task<TournamentParticipants> AddTournamentParticipantAsync(int idPlayer, int idTournament)
        {
            TournamentParticipants tournamentParticipants = new TournamentParticipants
            {
                IdPlayer = idPlayer,
                IdTournament = idTournament
            };

            Database.db.TournamentParticipants.Add(tournamentParticipants);
            await Database.db.SaveChangesAsync();
            return tournamentParticipants;
        }
    }
}
