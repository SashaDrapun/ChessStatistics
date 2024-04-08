using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System;
using System.Threading.Tasks;

namespace ChessStatistics.Services.GameServices
{
    public static class GameUpdater
    {
        public static async Task UpdateGameAsync(GameModel gameModel)
        {
            Game game = GameSearcher.GetGame(gameModel.IdGame);
            Tour tour = TourSearcher.GetTourById(game.IdTour);
            Tournament tournament = TournamentSearcher.GetTournamentById(tour.IdTournament);
            RatingType ratingType = tournament.RatingType;

            game.RatingWhite = PlayerSearcher.GetPlayerRating(game.IdPlayerWhite, ratingType);
            game.RatingBlack = PlayerSearcher.GetPlayerRating(game.IdPlayerBlack, ratingType);

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

            await PlayerUpdater.UpdateRatingPlayerAsync(game.IdPlayerWhite, Math.Round(game.RatingWhite + game.RatingWhiteChange, 2), ratingType);
            await PlayerUpdater.UpdateRatingPlayerAsync(game.IdPlayerBlack, Math.Round(game.RatingBlack + game.RatingBlackChange,  2), ratingType);
            Database.db.Games.Update(game);
            await Database.db.SaveChangesAsync();
        }
    }
}
