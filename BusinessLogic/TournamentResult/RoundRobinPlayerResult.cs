namespace ChessStatistics.BusinessLogic.TournamentResult
{
    public class RoundRobinPlayerResult
    {
        public RoundRobinPlayerResult()
        {

        }

        public RoundRobinPlayerResult(int id, string fIO, double rating)
        {
            Id = id;
            FIO = fIO;
            Rating = rating;
        }

        public int Id { get; set; }

        public string FIO { get; set; }

        public double Rating { get; set; }

        public int Place { get; set; }

        public double Points { get; set; }

        public double CoefficientPersonalMeeting { get; set; }

        public double CoefficientKoya { get; set; }

        public double СoefficientSonnebornBerger { get; set; }

        public int NumberOfWonGames { get; set; }
    }
}
