using ChessStatistics.Models.Enum;
using System;

namespace ChessStatistics.ViewModels
{
    public class Rating
    {
        public Rating(double ratingBlitz, double ratingRapid, double ratingClassic)
        {
            RatingBlitz = ratingBlitz;
            RatingRapid = ratingRapid;
            RatingClassic = ratingClassic;
        }

        public Rating(double ratingBlitz, double ratingRapid, double ratingClassic, RatingType ratingType) : this(ratingBlitz, ratingRapid, ratingClassic)
        {
            RatingType = ratingType;
        }

        public Rating() 
        {
           
        }

        //public double GetRating()
        //{
        //    double result = 0;

        //    if (RatingType == RatingType.Blitz) 
        //    {
        //        result = RatingBlitz;
        //    }

        //    if (RatingType == RatingType.Rapid)
        //    {
        //        result = RatingRapid;
        //    }

        //    if (RatingType == RatingType.Classic)
        //    {
        //        result = RatingClassic;
        //    }

        //    return Math.Round(result, 2);
        //}

        //public string GetRatingName()
        //{
        //    string result = string.Empty;

        //    if (RatingType == RatingType.Blitz)
        //    {
        //        result = "Рейтинг блиц";
        //    }

        //    if (RatingType == RatingType.Rapid)
        //    {
        //        result = "Рейтинг рапид";
        //    }

        //    if (RatingType == RatingType.Classic)
        //    {
        //        result = "Рейтинг классика";
        //    }

        //    return result;
        //}


        public double RatingBlitz { get; set; }

        public double RatingRapid { get; set; }

        public double RatingClassic { get; set; }

        public RatingType RatingType { get; set; }
    }
}
