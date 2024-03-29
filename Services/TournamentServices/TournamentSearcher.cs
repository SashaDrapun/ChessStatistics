﻿using ChessStatistics.BusinessLogic;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System.Collections.Generic;
using System.Linq;


namespace ChessStatistics.Services.TournamentServices
{
    public class TournamentSearcher
    {
        public static Tournament GetTournamentById(int IdTournament)
        {
            return Database.db.Tournaments.FirstOrDefault(t => t.Id == IdTournament);
        }

        public static TournamentDrawModel GetTournamentDraw(int idTournament)
        {
            TournamentDrawModel tournamentDraw = new TournamentDrawModel
            {
                Tours = new List<TourModel>()
            };

            List<Tour> tours = TourSearcher.GetToursByTournament(idTournament);

            for (int i = 0; i < tours.Count; i++)
            {
                TourModel tourModel = new TourModel
                {
                    Id = tours[i].Id,
                    TourNumber = tours[i].TourNumber,
                    IdTournament = tours[i].IdTournament,
                    IdPlayerSkippingGame = tours[i].IdPlayerSkippingGame,
                    Games = GameMapper.MapGames(GameSearcher.GetGamesByTour(tours[i].Id))
                };

                tournamentDraw.Tours.Add(tourModel);
            }

            return tournamentDraw;
        }

        public static TournamentModel GetTournamentModelById(int IdTournament)
        {
            Tournament tournament = Database.db.Tournaments.FirstOrDefault(t => t.Id == IdTournament);

            TournamentModel tournamentModel = new TournamentModel()
            {
                IdTournament = tournament.Id,
                CountTours = tournament.CountTours,
                DateStart = tournament.DateStart,
                TournamentDrawModel = GetTournamentDraw(tournament.Id),
                TournamentName = tournament.TournamentName,
                Type = tournament.Type
            };

            return tournamentModel;
        }
    }
}
