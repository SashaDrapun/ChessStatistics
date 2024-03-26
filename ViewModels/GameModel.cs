using ChessStatistics.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.ViewModels
{
    public class GameModel
    {
        public int Id { get; set; }
        public int IdPlayerWhite { get; set; }

        public int IdPlayerBlack { get; set; }

        public int IdTournament { get; set; }

        public int IdTour { get; set; }

        public GameResult GameResult { get; set; }

        public DateTime Date { get; set; }

        public Player PlayerWhite { get; set; }

        public Player PlayerBlack { get; set; }

        public double RatingWhite { get; set; }

        public double RatingBlack { get; set; }

        public double RatingWhiteChange { get; set; }

        public double RatingBlackChange { get; set; }

        public bool DidTheGamePassed { get; set; }

    }
}
