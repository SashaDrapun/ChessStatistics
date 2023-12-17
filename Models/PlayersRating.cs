using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Models
{
    public class PlayersRating
    {
        public PlayersRating(double whiteRating, double blackRating)
        {
            WhiteRating = whiteRating;
            BlackRating = blackRating;
        }

        public double WhiteRating { get; set; }

        public double BlackRating { get; set; }
    }
}
