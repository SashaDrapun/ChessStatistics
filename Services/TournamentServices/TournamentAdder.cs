using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
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
                City = tournamentModel.City,
                Adress = tournamentModel.Address,
                OnlineOrOffline = tournamentModel.OnlineOffline,
                Platform = tournamentModel.Platform,
                Link = tournamentModel.TournamentLink,
                MinimumYearOfBirth = tournamentModel.MinYear,
                MaxRating = tournamentModel.MaxRating,
                MaxCountOfPlayers = tournamentModel.MaxCountPlayers,
                IsTheTournamentHeldUsingThePlatform = tournamentModel.IsPlatformCalculated
            };

            if (tournamentModel.TournamentType == TournamentType.Round)
            {
                tournament.CountTours = 2;
            }

            Database.db.Tournaments.Add(tournament);
            await Database.db.SaveChangesAsync();
            return tournament;
        }
    }
}
