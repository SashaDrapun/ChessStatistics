using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentParticipantsServices
{
    public static class TournamentParticipantsSearcher
    {
        public static List<TournamentParticipants> GetAllTournamentParticipants()
        {
            return Database.db.TournamentParticipants.ToList();
        }

        public static List<int> GetTournamentParticipantsByTournamentId(int tournamentId)
        {
            return Database.db.TournamentParticipants.Where(tp => tp.IdTournament == tournamentId).Select(tp => tp.IdPlayer).ToList();
        }
    }
}
