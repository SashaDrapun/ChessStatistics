using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace ChessStatistics.Services.GameServices
{
    public static class GameAdder
    {
        public static async Task<Game> AddPassedGameAsync(GameModel gameModel)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(gameModel.IdTournament);
            RatingType ratingType = tournament.RatingType;

            double ratingWhite = PlayerSearcher.GetPlayerRating(gameModel.IdPlayerWhite, ratingType);
            double ratingBlack = PlayerSearcher.GetPlayerRating(gameModel.IdPlayerBlack, ratingType);

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
                RatingBlackChange = Math.Round(newPlayersRating.BlackRating - ratingBlack, 2),
                DidTheGamePassed = true
            };
            
            Database.db.Games.Add(game);
            await Database.db.SaveChangesAsync();

            await PlayerUpdater.UpdateRatingPlayerAsync(gameModel.IdPlayerWhite, newPlayersRating.WhiteRating, ratingType);
            await PlayerUpdater.UpdateRatingPlayerAsync(gameModel.IdPlayerBlack, newPlayersRating.BlackRating, ratingType);

            return game;
        }

        public static async Task<Game> AddNotPassedGameAsync(GameModel gameModel)
        {
            Game game = new Game
            {
                IdPlayerWhite = gameModel.IdPlayerWhite,
                IdPlayerBlack = gameModel.IdPlayerBlack,
                DidTheGamePassed = false,
                RatingWhite = gameModel.RatingWhite,
                RatingBlack = gameModel.RatingBlack,
                IdTour = gameModel.IdTour
            };

            Database.db.Games.Add(game);
            await Database.db.SaveChangesAsync();

            return game;
        }
    }
}
