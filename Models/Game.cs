using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class Game
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdGame { get; set; }

        [ForeignKey("Player")]
        public int IdPlayerWhite { get; set; }

        [ForeignKey("Player")]
        public int IdPlayerBlack { get; set; }

        [ForeignKey("Tour")]
        
        public int IdTour { get; set; }

        public GameResult GameResult { get; set; }

        public DateTime Date { get; set; }

        public double RatingWhite { get; set; }

        public double RatingBlack { get; set; }

        public double RatingWhiteChange { get; set; }

        public double RatingBlackChange { get; set; }

        public bool DidTheGamePassed { get; set; }


    }
}
