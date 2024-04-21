using System;


namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public class SwissPlayer
    {
        public SwissPlayer(int id, string name, double rating, double score)
        {
            Name = name;
            Rating = rating;
            Score = score;
            Id = id;
            CountGamePlayingWithWhite = 0;
            CountGamePlayingWithBlack = 0;
            DidMissTheTour = false;
            ColorPreviestGame = string.Empty;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public double Score { get; set; }

        public int CountGamePlayingWithWhite { get; set; }

        public int CountGamePlayingWithBlack { get; set; }

        public string ColorPreviestGame { get; set; }

        public bool DidMissTheTour { get; set; }
    }
}
