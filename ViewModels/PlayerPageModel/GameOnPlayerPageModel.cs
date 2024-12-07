using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.ViewModels.PlayerPageModel
{
    public class GameOnPlayerPageModel
    {

        public GameOnPlayerPageModel()
        {

        }

        public int IdGame { get; set; }

        public int IdYourOpponent { get; set; }

        public string YourColor { get; set; }

        public int IdTournament { get; set; }

        public int TourNumber { get; set; }

        public string GameResult { get; set; }

        public string DateInOutputFormat { get; set; }

        public string FIOYourOpponent { get; set; }

        public string YourRatingInMomentOfTheGame { get; set; }

        public string RatingYourOpponent { get; set; }

        public string YourRatingChanged { get; set; }

    }
}
