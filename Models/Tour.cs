using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class Tour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdTour { get; set; }

        [ForeignKey("Tournament")]
        public int IdTournament { get; set; }

        public int TourNumber { get; set; }

        public int IdPlayerSkippingGame { get; set; } 

    }
}
