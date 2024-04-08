using ChessStatistics.Models.Enum;
using ChessStatistics.ViewModels;
using System;

namespace ChessStatistics.Services
{
    public static class RatingOperations
    {
        public static double GetRating(Rating rating)
        {
            double result = 0;

            if (rating.RatingType == RatingType.Blitz)
            {
                result = rating.RatingBlitz;
            }

            if (rating.RatingType == RatingType.Rapid)
            {
                result = rating.RatingRapid;
            }

            if (rating.RatingType == RatingType.Classic)
            {
                result = rating.RatingClassic;
            }

            return Math.Round(result, 2);
        }
    }
}
