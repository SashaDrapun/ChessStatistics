using ChessStatistics.BusinessLogic;
using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.PlayerServices;
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
            return Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == IdTournament);
        }

        public static TournamentType GetTournamentTypeById(int IdTournament)
        {
            return Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == IdTournament).TournamentType;
        }

        public static RatingType GetTournamentRatingTypeById(int IdTournament)
        {
            return Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == IdTournament).RatingType;
        }

        public static TournamentDrawModel GetTournamentDraw(int idTournament)
        {
            TournamentDrawModel tournamentDraw = new()
            {
                Tours = []
            };

            List<Tour> tours = TourSearcher.GetToursByTournament(idTournament);

            for (int i = 0; i < tours.Count; i++)
            {
                TourModel tourModel = new()
                {
                    IdTour = tours[i].IdTour,
                    TourNumber = tours[i].TourNumber,
                    IdTournament = tours[i].IdTournament,
                    IdPlayerSkippingGame = tours[i].IdPlayerSkippingGame,
                    Games = GameMapper.MapGames(GameSearcher.GetGamesByTour(tours[i].IdTour))
                };

                if (tourModel.IdPlayerSkippingGame > 0)
                {
                    tourModel.PlayerSkippingGame = PlayerMapper.MapPlayer(PlayerSearcher.GetPlayerById(tourModel.IdPlayerSkippingGame), idTournament);
                    tourModel.PlayerSkippingGameScore = PlayerSearcher.GetPlayerScore(tourModel.IdPlayerSkippingGame, tourModel.IdTour);
                }

                tournamentDraw.Tours.Add(tourModel);

                bool allGamesPassed = tourModel.Games.All(game => game.DidTheGamePassed);

                if (!allGamesPassed)
                {
                    break;
                }
            }

            return tournamentDraw;
        }

        public static TournamentModel GetPartialTournamentModel(int idTournament)
        {
            Tournament tournament = Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == idTournament);

            TournamentModel tournamentModel = new()
            {
                IdTournament = tournament.IdTournament,
                CountTours = tournament.CountTours,
                DateStart = tournament.DateStart,
                TournamentName = tournament.TournamentName,
                RatingType = tournament.RatingType,
                TournamentType = tournament.TournamentType,
                City = tournament.City,
                Address = tournament.Adress,
                OnlineOffline = tournament.OnlineOrOffline,
                Platform = tournament.Platform,
                TournamentLink = tournament.Link,
                MinYear = tournament.MinimumYearOfBirth,
                MaxRating = tournament.MaxRating,
                MaxCountPlayers = tournament.MaxCountOfPlayers,
                IsPlatformCalculated = tournament.IsTheTournamentHeldUsingThePlatform,
                Cost = tournament.Cost
            };

            return tournamentModel;
        }

        public static TournamentModel GetTournamentModelById(int idTournament)
        {
            Tournament tournament = Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == idTournament);

            TournamentModel tournamentModel = new()
            {
                IdTournament = tournament.IdTournament,
                CountTours = tournament.CountTours,
                DateStart = tournament.DateStart,
                TournamentDrawModel = GetTournamentDraw(tournament.IdTournament),
                TournamentName = tournament.TournamentName,
                RatingType = tournament.RatingType,
                TournamentType = tournament.TournamentType,
                PlayersParticipatingInTournament = PlayerMapper.MapListPlayersToListPlayerOnPlayersPage(PlayerSearcher.
                GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, true)),
                PlayersNotParticipatingInTournament = PlayerMapper.MapListPlayersToListPlayerOnPlayersPage(PlayerSearcher.
                GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, false))
            };

            return tournamentModel;
        }

        public static TournamentModel SetRoundRobitResult(TournamentModel model)
        {
            GenerateTournamentResult generateTournamentResult = new(model.IdTournament);

            model.RoundRobinTournamentResult = generateTournamentResult.GenerateRoundRobinTournamentResult();

            return model;
        }

        public static RoundRobinTournamentResult GetRoundRobitResult(int idTournament)
        {
            GenerateTournamentResult generateTournamentResult = new(idTournament);

            return generateTournamentResult.GenerateRoundRobinTournamentResult();
        }

        public static int GetTournamentIdByTourId(int tourId)
        {
            return TourSearcher.GetTourById(tourId).IdTournament;
        }
    }
}
