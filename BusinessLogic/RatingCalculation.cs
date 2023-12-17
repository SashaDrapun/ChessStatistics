using ChessStatistics.Models;
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

        public static double GetStartRating(string title)
        {
            if (title == "4 разряд")
            {
                return 1200;
            }

            if (title == "3 разряд")
            {
                return 1400;
            }

            if (title == "2 разряд")
            {
                return 1600;
            }

            if (title == "1 разряд")
            {
                return 1800;
            }

            if (title == "КМС")
            {
                return 2000;
            }

            if (title == "Мастер ФИДЕ")
            {
                return 2300;
            }

            if (title == "Международный мастер")
            {
                return 2400;
            }

            return 2500;
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
