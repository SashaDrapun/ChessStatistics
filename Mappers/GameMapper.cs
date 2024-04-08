using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System.Collections.Generic;

namespace ChessStatistics.Mappers
{
    public static class GameMapper
    {
        public static GameModel MapGame(Game game)
        {
            Tour tour = TourSearcher.GetTourById(game.IdTour);
            Tournament tournament = TournamentSearcher.GetTournamentById(tour.IdTournament);
            RatingType ratingType = tournament.RatingType;

            GameModel model = new GameModel()
            {
                IdPlayerWhite = game.IdPlayerWhite,
                IdPlayerBlack = game.IdPlayerBlack,
                IdTour = game.IdTour,
                IdTournament = tournament.IdTournament,
                IdGame = game.IdGame,
                Date = game.Date,
                DidTheGamePassed = game.DidTheGamePassed,
                GameResult = game.GameResult,
                RatingWhiteChange = game.RatingWhiteChange,
                RatingBlackChange = game.RatingBlackChange,
                
            };

            model.RatingWhite = PlayerSearcher.GetPlayerRating(model.IdPlayerWhite, ratingType);
            model.RatingBlack = PlayerSearcher.GetPlayerRating(model.IdPlayerBlack, ratingType);
            model.PlayerWhite = PlayerSearcher.GetPlayerById(model.IdPlayerWhite);
            model.PlayerBlack = PlayerSearcher.GetPlayerById(model.IdPlayerBlack);

            return model;
        }

        public static List<GameModel> MapGames(List<Game> games)
        {
            List<GameModel> gameModels = new List<GameModel>();

            for (int i = 0; i < games.Count; i++)
            {
                gameModels.Add(MapGame(games[i]));
            }

            return gameModels;
        }
    }
}
