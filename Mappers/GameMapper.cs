using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ChessStatistics.Mappers
{
    public static class GameMapper
    {
        public static GameModel MapGame(Game game)
        {
            Tour tour = TourSearcher.GetTourById(game.IdTour);
            Tournament tournament = TournamentSearcher.GetTournamentById(tour.IdTournament);
            List<Tour> tours = TourSearcher.GetToursByTournament(tour.IdTournament);
            RatingType ratingType = tournament.RatingType;

            double scoreWhite = 0;
            double scoreBlack = 0;

            GameModel model = new()
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

            for (int i = 0; i < tour.TourNumber - 1; i++)
            {
                List<Game> games = GameSearcher.GetGamesByTour(tours[i].IdTour);
                
                foreach (var currentGame in games)
                {
                    if (!currentGame.DidTheGamePassed)
                    {
                        continue;
                    }
                    
                    if (currentGame.IdPlayerWhite == game.IdPlayerWhite)
                    {
                        if (currentGame.GameResult == GameResult.WhiteWin)
                        {
                            scoreWhite++;
                        }

                        if (currentGame.GameResult == GameResult.Draw)
                        {
                            scoreWhite += 0.5;
                        }
                    }

                    if (currentGame.IdPlayerBlack == game.IdPlayerWhite)
                    {
                        if (currentGame.GameResult == GameResult.BlackWin)
                        {
                            scoreWhite++;
                        }

                        if (currentGame.GameResult == GameResult.Draw)
                        {
                            scoreWhite += 0.5;
                        }
                    }

                    if (currentGame.IdPlayerWhite == game.IdPlayerBlack)
                    {
                        if (currentGame.GameResult == GameResult.WhiteWin)
                        {
                            scoreBlack++;
                        }

                        if (currentGame.GameResult == GameResult.Draw)
                        {
                            scoreBlack += 0.5;
                        }
                    }

                    if (currentGame.IdPlayerBlack == game.IdPlayerBlack)
                    {
                        if (currentGame.GameResult == GameResult.BlackWin)
                        {
                            scoreBlack++;
                        }

                        if (currentGame.GameResult == GameResult.Draw)
                        {
                            scoreBlack += 0.5;
                        }
                    }
                }

                if (tours[i].IdPlayerSkippingGame == game.IdPlayerWhite)
                {
                    scoreWhite++;
                }

                if (tours[i].IdPlayerSkippingGame == game.IdPlayerBlack)
                {
                    scoreBlack++;
                }
            }

            model.RatingWhite = PlayerSearcher.GetPlayerRating(model.IdPlayerWhite, ratingType);
            model.RatingBlack = PlayerSearcher.GetPlayerRating(model.IdPlayerBlack, ratingType);
            model.PlayerWhite = PlayerSearcher.GetPlayerById(model.IdPlayerWhite);
            model.PlayerBlack = PlayerSearcher.GetPlayerById(model.IdPlayerBlack);
            model.PlayerWhiteScore = scoreWhite;
            model.PlayerBlackScore = scoreBlack;

            return model;
        }

        public static List<GameModel> MapGames(List<Game> games)
        {
            List<GameModel> gameModels = [];

            for (int i = 0; i < games.Count; i++)
            {
                gameModels.Add(MapGame(games[i]));
            }

            return gameModels;
        }

        public static GameOnPlayerPageModel MapGameIntoGameOnPlayerPageModel(Game game, Player currentPlayer)
        {
            GameOnPlayerPageModel gameOnPlayerPageModel = new GameOnPlayerPageModel();
            gameOnPlayerPageModel.IdGame = game.IdGame;
            gameOnPlayerPageModel.IdYourOpponent = game.IdPlayerWhite == currentPlayer.IdPlayer ? game.IdPlayerBlack : game.IdPlayerWhite;
            gameOnPlayerPageModel.YourColor = game.IdPlayerWhite == currentPlayer.IdPlayer ? "Белые" : "Черные";
            gameOnPlayerPageModel.IdTournament = TournamentSearcher.GetTournamentIdByTourId(game.IdTour);
            gameOnPlayerPageModel.TourNumber = TourSearcher.GetTourNumberByTourId(game.IdTour);
            gameOnPlayerPageModel.GameResult = game.GameResult == GameResult.WhiteWin ? "1-0" : game.GameResult == GameResult.Draw ? "1/2" : "0-1";
            gameOnPlayerPageModel.DateInOutputFormat = game.Date.ToString("dd MMMM yyyy HH:mm", new CultureInfo("ru-RU"));
            gameOnPlayerPageModel.FIOYourOpponent = PlayerSearcher.GetPlayerFIOById(gameOnPlayerPageModel.IdYourOpponent);
            gameOnPlayerPageModel.YourRatingInMomentOfTheGame = ((int)Math.Round(game.IdPlayerWhite == currentPlayer.IdPlayer ? game.RatingWhite : game.RatingBlack)).ToString();
            gameOnPlayerPageModel.RatingYourOpponent = ((int)Math.Round(game.IdPlayerWhite != currentPlayer.IdPlayer ? game.RatingWhite : game.RatingBlack)).ToString();
            gameOnPlayerPageModel.YourRatingChanged = (game.IdPlayerWhite == currentPlayer.IdPlayer ? game.RatingWhiteChange: game.RatingBlackChange).ToString("F2");
            return gameOnPlayerPageModel;
        }

        public static List<GameOnPlayerPageModel> MapGamesIntoGamesOnPlayerPageModel(List<Game> games, Player currentPlayer)
        {
            var result = new List<GameOnPlayerPageModel>();
            foreach (var game in games)
            {
                result.Add(MapGameIntoGameOnPlayerPageModel(game, currentPlayer));
            }

            return result;
        }
    }
}
