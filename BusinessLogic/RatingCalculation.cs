using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using System;

namespace ChessStatistics.BusinessLogic
{
    public static class RatingCalculation
    {
        public static PlayersRating ReturnNewPlayersRating(double ratingWhite, double ratingBlack, GameResult gameResult)
        {
            bool IsWhiteWeaker = ratingWhite - ratingBlack < 0;

            int ratingDifference = Math.Abs((int)Math.Round(ratingWhite - ratingBlack));

            double highestCoefficient = GetHighestCoefficient(ratingDifference);
            double lowestCoefficient = Math.Round(1 - highestCoefficient, 2);

            double coefficientWhite = IsWhiteWeaker ? lowestCoefficient : highestCoefficient;
            double coefficientBlack = IsWhiteWeaker ? highestCoefficient : lowestCoefficient;

            double newRatingWhite = ratingWhite + (20 * (GetDigitalRepresentationOfResult(gameResult, true) - coefficientWhite));
            double newRatingBlack = ratingBlack + (20 * (GetDigitalRepresentationOfResult(gameResult, false) - coefficientBlack));

            return new PlayersRating(newRatingWhite, newRatingBlack);
        }

        public static double GetStartRating(Rank rank)
        {
            if (rank == Rank.Fourth)
            {
                return 1200;
            }

            if (rank == Rank.Third)
            {
                return 1400;
            }

            if (rank == Rank.Second)
            {
                return 1600;
            }

            if (rank == Rank.First)
            {
                return 1800;
            }

            if (rank == Rank.Kms)
            {
                return 2000;
            }

            if (rank == Rank.FM)
            {
                return 2300;
            }

            if (rank == Rank.IM)
            {
                return 2400;
            }

            if (rank == Rank.GM)
            {
                return 2500;
            }

            return 1000;
        }

        private static double GetDigitalRepresentationOfResult(GameResult gameResult, bool forWhites)
        {
            if (gameResult == GameResult.WhiteWin)
            {
                return forWhites ? 1 : 0;
            }

            if (gameResult == GameResult.BlackWin)
            {
                return forWhites ? 0 : 1;
            }

            return 0.5;
        }

        private static double GetHighestCoefficient(int ratingDifference)
        {
            if (ratingDifference < 25)
            {
                return 0.5;
            }

            if (ratingDifference < 50)
            {
                return 0.53;
            }

            if (ratingDifference < 100)
            {
                return 0.57;
            }

            if (ratingDifference < 150)
            {
                return 0.64;
            }

            if (ratingDifference < 200)
            {
                return 0.7;
            }

            if (ratingDifference < 250)
            {
                return 0.76;
            }

            if (ratingDifference < 300)
            {
                return 0.81;
            }

            if (ratingDifference < 350)
            {
                return 0.85;
            }

            if (ratingDifference < 400)
            {
                return 0.89;
            }

            return 0.92;
        }
    }
}
