using ChessStatistics.Models;
using System.Collections.Generic;

namespace ChessStatistics.ViewModels
{
    public class TourModel
    {
        public int Id { get; set; }

        public int IdTournament { get; set; }

        public int TourNumber { get; set; }

        public int IdPlayerSkippingGame { get; set; }

        public Player PlayerSkippingGame { get; set; }

        public List<GameModel> Games { get; set; }
    }
}
