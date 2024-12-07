﻿using ChessStatistics.Models;
using System.Collections.Generic;

namespace ChessStatistics.ViewModels.TournamentPage
{
    public class TourModel
    {
        public int IdTour { get; set; }

        public int IdTournament { get; set; }

        public int TourNumber { get; set; }

        public int IdPlayerSkippingGame { get; set; }

        public PlayerModel PlayerSkippingGame { get; set; }

        public double PlayerSkippingGameScore { get; set; }

        public List<GameModel> Games { get; set; }
    }
}
