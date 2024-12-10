using ChessStatistics.Models.Enum;

namespace ChessStatistics.ViewModels.TournamentsPage
{
    public enum DateStatus
    {
        Past,
        Current,
        Planned
    }

    public class TournamentFilter
    {
        public DateStatus DateStatus { get; set; }

        public OnlineOrOffline OnlineOrOffline { get; set; }

        public string City { get; set; }    

        public int MaxRating { get; set; }
        
        public int MaxAge { get; set; }
    }
}
