using ChessStatistics.Models;

namespace ChessStatistics.ViewModels.TournamentPage
{
    public class SetGameResultModel
    {
        public int IdGame { get; set; }
        public GameResult GameResult { get; set; }
        public int IdTournament { get; set; }

        public int TabPosition { get; set; }

        public int TourPosition { get; set; }
    }
}
