using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using System;
using System.Threading.Tasks;

namespace ChessStatistics.Services.GameServices
{
    public static class GameUpdater
    {
        public static async Task UpdateGameAsync(GameModel gameModel)
        {
            Game game = GameSearcher.GetGame(gameModel.Id);

            game.RatingWhite = PlayerSearcher.GetPlayerRating(game.IdPlayerWhite);
            game.RatingBlack = PlayerSearcher.GetPlayerRating(game.IdPlayerBlack);

            if (game.DidTheGamePassed)
            {
                game.RatingWhite = Math.Round(game.RatingWhite - game.RatingWhiteChange, 2);
                game.RatingBlack = Math.Round(game.RatingBlack - game.RatingBlackChange, 2);
            }

            PlayersRating newPlayersRating = RatingCalculation.ReturnNewPlayersRating(game.RatingWhite, game.RatingBlack, gameModel.GameResult);
            game.GameResult = gameModel.GameResult;
            
            game.RatingWhiteChange = Math.Round(newPlayersRating.WhiteRating - game.RatingWhite, 2);
            game.RatingBlackChange = Math.Round(newPlayersRating.BlackRating - game.RatingBlack, 2);
            game.DidTheGamePassed = true;
            game.Date = DateTime.Now;

            await PlayerUpdater.UpdateRatingPlayerAsync(game.IdPlayerWhite, Math.Round(game.RatingWhite + game.RatingWhiteChange, 2));
            await PlayerUpdater.UpdateRatingPlayerAsync(game.IdPlayerBlack, Math.Round(game.RatingBlack + game.RatingBlackChange,  2));
            Database.db.Games.Update(game);
            await Database.db.SaveChangesAsync();
        }
    }
}
