using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System.Collections.Generic;

namespace ChessStatistics.Mappers
{
    public static class GameMapper
    {
        public static GameModel MapGame(Game game)
        {
            GameModel model = new GameModel
            {
                Id = game.Id,
                IdPlayerWhite = game.IdPlayerWhite,
                IdPlayerBlack = game.IdPlayerBlack,
                Date = game.Date,
                DidTheGamePassed = game.DidTheGamePassed,
                GameResult = game.GameResult,
                RatingBlack = game.RatingBlack,
                RatingWhite = game.RatingWhite,
                RatingBlackChange = game.RatingBlackChange,
                RatingWhiteChange = game.RatingWhiteChange,
                PlayerWhite = PlayerSearcher.GetPlayerById(game.IdPlayerWhite),
                PlayerBlack = PlayerSearcher.GetPlayerById(game.IdPlayerBlack),
                IdTour = game.IdTour,
                IdTournament = TourSearcher.GetTourById(game.IdTour).IdTournament,
            };


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
