using ChessStatistics.Models.Enum;
using System;
using TournamentTypeT = ChessStatistics.Models.Enum.TournamentType;
using RatingTypeT = ChessStatistics.Models.Enum.RatingType;

namespace ChessStatistics.ViewModels.TournamentsPage
{
    public class AddTournamentModel
    {

        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }

        public TournamentTypeT TournamentType { get; set; }

        public RatingTypeT RatingType { get; set; }

        public int CountTours { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public OnlineOrOffline OnlineOffline { get; set; }

        public string Platform { get; set; }

        public string TournamentLink { get; set; }

        public int MinYear { get; set; }

        public int MaxRating { get; set; }

        public bool IsPlatformCalculated { get; set; }

        public int MaxCountPlayers { get; set; }

        public Cost Cost { get; set; }
    }
}
