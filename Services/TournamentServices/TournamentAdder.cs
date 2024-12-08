﻿using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.ViewModels;
using ChessStatistics.ViewModels.TournamentsPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentServices
{
    public static class TournamentAdder
    {
        public static async Task<Tournament> AddTournamentAsync(AddTournamentModel tournamentModel)
        {
            Tournament tournament = new Tournament
            {
                TournamentName = tournamentModel.TournamentName,
                DateStart = tournamentModel.DateStart,
                DateFinish = tournamentModel.DateFinish,
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
                IsTheTournamentHeldUsingThePlatform = tournamentModel.IsPlatformCalculated,
                Cost = tournamentModel.Cost
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
