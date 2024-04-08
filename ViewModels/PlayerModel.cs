namespace ChessStatistics.ViewModels
{
    public class PlayerModel
    {
        public int IdPlayer { get; set; }

        public string FIO { get; set; }

        public string Rank { get; set; }

        public double CurrentRating { get; set; }

        public Rating Rating { get; set; }
    }
}
