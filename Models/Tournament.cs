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

        public string City { get; set; }

        public string Adress { get; set; }

        public OnlineOrOffline OnlineOrOffline { get; set; }

        public string Platform { get; set; }

        public string Link { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }

        public TournamentType TournamentType { get; set; }

        public RatingType RatingType { get; set; }

        public int CountTours { get; set; }

        public bool IsInProgress { get; set; }

        public int MinimumYearOfBirth { get; set; }

        public int MaxRating { get; set; }

        public int MaxCountOfPlayers {  get; set; }

        public bool IsTheTournamentHeldUsingThePlatform { get; set; }

        public Cost Cost {get; set;}
    }
}