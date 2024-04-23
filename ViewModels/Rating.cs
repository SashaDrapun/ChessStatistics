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

        public double RatingBlitz { get; set; }

        public double RatingRapid { get; set; }

        public double RatingClassic { get; set; }

        public RatingType RatingType { get; set; }
    }
}
