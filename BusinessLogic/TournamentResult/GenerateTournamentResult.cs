using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using System;
using System.Linq;

namespace ChessStatistics.BusinessLogic.TournamentResult
{
    public class GenerateTournamentResult
    {
        private TournamentModel tournamentModel { get; set; }

        private RoundRobinTournamentResult result { get; set; }

        public GenerateTournamentResult(int idTournament)
        {
            tournamentModel = TournamentSearcher.GetTournamentModelById(idTournament);
            result = new RoundRobinTournamentResult();
        }

        public RoundRobinTournamentResult GenerateRoundRobinTournamentResult()
        {
            bool IsAllGamesPlayed = true;

            if (tournamentModel.TournamentDrawModel.Tours.Count == 0)
            {
                IsAllGamesPlayed = false;
            }

            foreach (var tour in tournamentModel.TournamentDrawModel.Tours)
            {
                foreach (var game in tour.Games)
                {
                    if (!game.DidTheGamePassed)
                    {
                        IsAllGamesPlayed = false;
                        break;
                    }
                }
            }

            if (IsAllGamesPlayed)
            {
                result.IsResultReady = true;
                SetPlayers();
                CalculateCountPointAndCountWonGames();
                CalculateCoefficients();
                result.Players = result.Players.OrderByDescending(x => x.Points).
                               ThenByDescending(x => x.CoefficientPersonalMeeting).
                               ThenByDescending(x => x.CoefficientKoya).
                               ThenByDescending(x => x.СoefficientSonnebornBerger).
                               ThenByDescending(x => x.NumberOfWonGames).ToList();
                for (int i = 0; i < result.Players.Count; i++)
                {
                    result.Players[i].Place = i + 1;
                }
            }
            else
            {
                result.IsResultReady = false;
            }

            return result;
        }

        private void SetPlayers()
        {
            foreach (var player in tournamentModel.PlayersParticipatingInTournament)
            {
                result.Players.Add(new RoundRobinPlayerResult(player.IdPlayer, player.FIO, ChoouseRatingByType(player)));
            }
        }

        private double ChoouseRatingByType(Player player)
        {
            double playerRating = 0;

            if (tournamentModel.RatingType == RatingType.Blitz)
            {
                playerRating = player.RatingBlitz;
            }

            if (tournamentModel.RatingType == RatingType.Rapid)
            {
                playerRating = player.RatingRapid;
            }

            if (tournamentModel.RatingType == RatingType.Classic)
            {
                playerRating = player.RatingClassic;
            }

            return playerRating;
        }

        private double ChoouseRatingByType(PlayerOnPagePlayersModel player)
        {
            double playerRating = 0;

            if (tournamentModel.RatingType == RatingType.Blitz)
            {
                playerRating = player.RatingBlitz;
            }

            if (tournamentModel.RatingType == RatingType.Rapid)
            {
                playerRating = player.RatingRapid;
            }

            if (tournamentModel.RatingType == RatingType.Classic)
            {
                playerRating = player.RatingClassic;
            }

            return playerRating;
        }

        private void CalculateCountPointAndCountWonGames()
        {
            foreach (var tour in tournamentModel.TournamentDrawModel.Tours)
            {
                foreach (var game in tour.Games)
                {
                    if (game.GameResult == GameResult.WhiteWin)
                    {
                        result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().Points++;
                        result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().NumberOfWonGames++;
                    }

                    if (game.GameResult == GameResult.Draw)
                    {
                        result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().Points += 0.5;
                        result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().Points += 0.5;
                    }

                    if (game.GameResult == GameResult.BlackWin)
                    {
                        result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().Points++;
                        result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().NumberOfWonGames++;
                    }
                }
            }
        }

        private void CalculateCoefficients()
        {
            foreach (var tour in tournamentModel.TournamentDrawModel.Tours)
            {
                foreach (var game in tour.Games)
                {
                    RoundRobinPlayerResult playerWhite = result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault();
                    RoundRobinPlayerResult playerBlack = result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault();

                    if (game.GameResult == GameResult.WhiteWin)
                    {
                        CalculateCoefficientsWhiteWin(playerWhite, playerBlack);
                    }

                    if (game.GameResult == GameResult.Draw)
                    {
                        CalculateCoefficientsDraw(playerWhite, playerBlack);
                    }

                    if (game.GameResult == GameResult.BlackWin)
                    {
                        CalculateCoefficientsBlackWin(playerWhite, playerBlack);
                    }
                }
            }
        }


        private void CalculateCoefficientsWhiteWin(RoundRobinPlayerResult playerWhite, RoundRobinPlayerResult playerBlack)
        {
            double halfOfMaxAmountOfPoints = (double)(tournamentModel.PlayersParticipatingInTournament.Count - 1) / 2;
            if (playerWhite.Points == playerBlack.Points)
            {
                playerWhite.CoefficientPersonalMeeting++;
            }

            if (playerBlack.Points >= halfOfMaxAmountOfPoints)
            {
                playerWhite.CoefficientKoya++;
            }

            playerWhite.СoefficientSonnebornBerger += playerBlack.Points * 2;
        }

        private void CalculateCoefficientsDraw(RoundRobinPlayerResult playerWhite, RoundRobinPlayerResult playerBlack)
        {
            double halfOfMaxAmountOfPoints = (double)(tournamentModel.PlayersParticipatingInTournament.Count - 1) / 2;

            if (playerWhite.Points == playerBlack.Points)
            {
                playerWhite.CoefficientPersonalMeeting += 0.5;
                playerBlack.CoefficientPersonalMeeting += 0.5;
            }

            if (playerBlack.Points >= halfOfMaxAmountOfPoints)
            {
                playerWhite.CoefficientKoya += 0.5;
            }

            if (playerWhite.Points >= halfOfMaxAmountOfPoints)
            {
                playerBlack.CoefficientKoya += 0.5;
            }

            playerWhite.СoefficientSonnebornBerger += playerBlack.Points;
            playerBlack.СoefficientSonnebornBerger += playerWhite.Points;
        }

        private void CalculateCoefficientsBlackWin(RoundRobinPlayerResult playerWhite, RoundRobinPlayerResult playerBlack)
        {
            double halfOfMaxAmountOfPoints = (double)(tournamentModel.PlayersParticipatingInTournament.Count - 1) / 2;

            if (playerWhite.Points == playerBlack.Points)
            {
                playerBlack.CoefficientPersonalMeeting += 1;
            }

            if (playerWhite.Points >= halfOfMaxAmountOfPoints)
            {
                playerBlack.CoefficientKoya++;
            }

            playerBlack.СoefficientSonnebornBerger += playerWhite.Points * 2;
        }
    }
}
