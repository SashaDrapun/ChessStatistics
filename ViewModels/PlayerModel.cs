﻿using ChessStatistics.Models.Enum;

namespace ChessStatistics.ViewModels
{
    public class PlayerModel
    {
        public int IdPlayer { get; set; }

        public string FIO { get; set; }

        public Rank Rank { get; set; }

        public string RankOutput { get; set; }

        public double CurrentRating { get; set; }

        public double CurrentRatingClassic { get; set; }

        public double CurrentRatingRapid { get; set; }

        public double CurrentRatingBlitz { get; set; }

        public Rating Rating { get; set; }

        public int IdTournament { get; set; }

        public byte[] Photo { get; set; }
    }
}
