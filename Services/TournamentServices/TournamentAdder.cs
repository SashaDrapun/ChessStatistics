using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentServices
{
    public static class TournamentAdder
    {
        public static async Task<Tournament> AddTournamentAsync(TournamentModel tournamentModel)
        {
            Tournament tournament = new Tournament
            {
                TournamentName = tournamentModel.TournamentName,
                DateStart = tournamentModel.DateStart,
                TournamentType = tournamentModel.TournamentType,
                RatingType = tournamentModel.RatingType,
                CountTours = tournamentModel.CountTours,
            };

            Database.db.Tournaments.Add(tournament);
            await Database.db.SaveChangesAsync();
            return tournament;
        }
    }
}
