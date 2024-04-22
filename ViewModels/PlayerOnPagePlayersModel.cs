﻿using ChessStatistics.Models.Enum;

namespace ChessStatistics.ViewModels
{
    public class PlayerOnPagePlayersModel
    {
        public int IdPlayer { get; set; }

        public string FIO { get; set; }

        public Rank Rank { get; set; }

        public string RankOutput { get; set; }

        public double RatingBlitz { get; set; }

        public double RatingRapid { get; set; }

        public double RatingClassic { get; set; }
    }
}
