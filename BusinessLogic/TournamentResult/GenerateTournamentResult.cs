using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using System;
using System.Linq;

namespace ChessStatistics.BusinessLogic.TournamentResult
{
    public class GenerateTournamentResult(int idTournament)
    {
        private TournamentModel TournamentModel { get; set; } = TournamentSearcher.GetTournamentModelById(idTournament);

        private RoundRobinTournamentResult Result { get; set; } = new RoundRobinTournamentResult();

        public RoundRobinTournamentResult GenerateRoundRobinTournamentResult()
        {
            bool IsTournamentStarted = true;

            if (TournamentModel.TournamentDrawModel.Tours.Count == 0)
            {
                IsTournamentStarted = false;
            }


            if (IsTournamentStarted)
            {
                Result.IsResultReady = true;
                SetPlayers();
                CalculateCountPointAndCountWonGames();
                CalculateCoefficients();
                Result.Players = [.. Result.Players.OrderByDescending(x => x.Points).
                               ThenByDescending(x => x.CoefficientPersonalMeeting).
                               ThenByDescending(x => x.CoefficientKoya).
                               ThenByDescending(x => x.СoefficientSonnebornBerger).
                               ThenByDescending(x => x.NumberOfWonGames)];
                for (int i = 0; i < Result.Players.Count; i++)
                {
                    Result.Players[i].Place = i + 1;
                }
            }
            else
            {
                Result.IsResultReady = false;
            }

            return Result;
        }

        private void SetPlayers()
        {
            foreach (var player in TournamentModel.PlayersParticipatingInTournament)
            {
                Result.Players.Add(new RoundRobinPlayerResult(player.IdPlayer, player.FIO, ChoouseRatingByType(player)));
            }
        }

        private double ChoouseRatingByType(PlayerOnPagePlayersModel player)
        {
            double playerRating = 0;

            if (TournamentModel.RatingType == RatingType.Blitz)
            {
                playerRating = player.RatingBlitz;
            }

            if (TournamentModel.RatingType == RatingType.Rapid)
            {
                playerRating = player.RatingRapid;
            }

            if (TournamentModel.RatingType == RatingType.Classic)
            {
                playerRating = player.RatingClassic;
            }

            return playerRating;
        }

        private void CalculateCountPointAndCountWonGames()
        {
            foreach (var tour in TournamentModel.TournamentDrawModel.Tours)
            {
                foreach (var game in tour.Games)
                {
                    if (!game.DidTheGamePassed)
                    {
                        continue;
                    }

                    if (game.GameResult == GameResult.WhiteWin)
                    {
                        Result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().Points++;
                        Result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().NumberOfWonGames++;
                    }

                    if (game.GameResult == GameResult.Draw)
                    {
                        Result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().Points += 0.5;
                        Result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().Points += 0.5;
                    }

                    if (game.GameResult == GameResult.BlackWin)
                    {
                        Result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().Points++;
                        Result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().NumberOfWonGames++;
                    }
                }
            }
        }

        private void CalculateCoefficients()
        {
            foreach (var tour in TournamentModel.TournamentDrawModel.Tours)
            {
                foreach (var game in tour.Games)
                {
                    RoundRobinPlayerResult playerWhite = Result.Players.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault();
                    RoundRobinPlayerResult playerBlack = Result.Players.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault();

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
            double halfOfMaxAmountOfPoints = (double)(TournamentModel.PlayersParticipatingInTournament.Count - 1) / 2;
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
            double halfOfMaxAmountOfPoints = (double)(TournamentModel.PlayersParticipatingInTournament.Count - 1) / 2;

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
            double halfOfMaxAmountOfPoints = (double)(TournamentModel.PlayersParticipatingInTournament.Count - 1) / 2;

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
