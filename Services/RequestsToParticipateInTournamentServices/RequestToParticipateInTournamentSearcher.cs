using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;

namespace ChessStatistics.Services.RequestsToParticipateInTournamentServices
{
    public static class RequestToParticipateInTournamentSearcher
    {
        public static List<TournamentRequests> GetAllTournamentRequests()
        {
            return Database.db.TournamentRequests.ToList();
        }

        public static List<TournamentRequests> GetAllTournamentInTournamentRequests(int tournamentId)
        {
            return Database.db.TournamentRequests.Where(request => request.IdTournament == tournamentId).ToList();
        }

        public static TournamentRequests GetTournamentRequestByUserIdAndTournamentId(string userId, int tournamentId)
        {
            return Database.db.TournamentRequests.FirstOrDefault(request => request.IdUser == userId && request.IdTournament == tournamentId);
        }
    }
}