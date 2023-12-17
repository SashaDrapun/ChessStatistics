using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.ViewModels;
using System;
using System.Threading.Tasks;

namespace ChessStatistics.Services.GameServices
{
    public static class GameAdder
    {
        public static async Task<Game> AddGameAsync(GameModel gameModel)
        {
            double ratingWhite = PlayerSearcher.GetPlayerRating(gameModel.IdPlayerWhite);
            double ratingBlack = PlayerSearcher.GetPlayerRating(gameModel.IdPlayerBlack);

            PlayersRating newPlayersRating = RatingCalculation.ReturnNewPlayersRating(ratingWhite, ratingBlack, gameModel.GameResult);

            Game game = new Game
            {
                IdPlayerWhite = gameModel.IdPlayerWhite,
                IdPlayerBlack = gameModel.IdPlayerBlack,
                Date = gameModel.Date,
                GameResult = gameModel.GameResult,
                RatingWhite = ratingWhite,
                RatingBlack = ratingBlack,
                RatingWhiteChange = Math.Round(newPlayersRating.WhiteRating - ratingWhite, 2),
                RatingBlackChange = Math.Round(newPlayersRating.BlackRating - ratingBlack, 2)
            };
            
            Database.db.Games.Add(game);
            await Database.db.SaveChangesAsync();

            await PlayerUpdater.UpdateRatingPlayerAsync(gameModel.IdPlayerWhite, newPlayersRating.WhiteRating);
            await PlayerUpdater.UpdateRatingPlayerAsync(gameModel.IdPlayerBlack, newPlayersRating.BlackRating);

            return game;
        }
    }
}
