using System;


namespace ChessStatistics.ViewModels
{
    public class TournamentModel
    {
        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }

        public string Type { get; set; }

        public int CountTours { get; set; }
    }
}
