using ChessStatistics.BusinessLogic;
using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
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
                    IdTour = tours[i].IdTour,
                    TourNumber = tours[i].TourNumber,
                    IdTournament = tours[i].IdTournament,
                    IdPlayerSkippingGame = tours[i].IdPlayerSkippingGame,
                    Games = GameMapper.MapGames(GameSearcher.GetGamesByTour(tours[i].IdTour))
                };

                if (tourModel.IdPlayerSkippingGame > 0)
                {
                    tourModel.PlayerSkippingGame = PlayerMapper.MapPlayer(PlayerSearcher.GetPlayerById(tourModel.IdPlayerSkippingGame), idTournament);
                }

                tournamentDraw.Tours.Add(tourModel);
            }

            return tournamentDraw;
        }

        public static TournamentModel GetPartialTournamentModel(int idTournament)
        {
            Tournament tournament = Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == idTournament);

            TournamentModel tournamentModel = new TournamentModel()
            {
                IdTournament = tournament.IdTournament,
                CountTours = tournament.CountTours,
                DateStart = tournament.DateStart,
                TournamentName = tournament.TournamentName,
                RatingType = tournament.RatingType,
                TournamentType = tournament.TournamentType,
            };

            return tournamentModel;
        }

        public static TournamentModel GetTournamentModelById(int idTournament)
        {
            Tournament tournament = Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == idTournament);

            TournamentModel tournamentModel = new TournamentModel()
            {
                IdTournament = tournament.IdTournament,
                CountTours = tournament.CountTours,
                DateStart = tournament.DateStart,
                TournamentDrawModel = GetTournamentDraw(tournament.IdTournament),
                TournamentName = tournament.TournamentName,
                RatingType = tournament.RatingType,
                TournamentType = tournament.TournamentType,
                PlayersParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, true),
                PlayersNotParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, false)
            };

            return tournamentModel;
        }

        public static TournamentModel SetRoundRobitResult(TournamentModel model)
        {
            GenerateTournamentResult generateTournamentResult = new GenerateTournamentResult(model.IdTournament);

            model.RoundRobinTournamentResult = generateTournamentResult.GenerateRoundRobinTournamentResult();

            return model;
        }
    }
}
