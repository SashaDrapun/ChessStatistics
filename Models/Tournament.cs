using ChessStatistics.Models.Enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class Tournament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTournament { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }

        public TournamentType TournamentType { get; set; }

        public RatingType RatingType { get; set; }

        public int CountTours { get; set; }

        public bool IsInProgress { get; set; }
    }
}
