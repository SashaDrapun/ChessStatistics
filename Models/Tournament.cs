using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class Tournament
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }

        public string Type { get; set; }

        public string CountTours { get; set; }

        public bool IsInProgress { get; set; }
    }
}
