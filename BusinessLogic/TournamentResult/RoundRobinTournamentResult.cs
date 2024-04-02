using ChessStatistics.Models;
using System.Collections.Generic;

namespace ChessStatistics.BusinessLogic.TournamentResult
{
    public class RoundRobinTournamentResult
    {
        public RoundRobinTournamentResult()
        {
            Players = new List<RoundRobinPlayerResult>();
        }

        public List<RoundRobinPlayerResult> Players { get; set; }

        public bool IsResultReady { get; set; }
    }
}
