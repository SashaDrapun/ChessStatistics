using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public class SwissGroup
    {
        public List<SwissPlayer> Players = new List<SwissPlayer>();

        public SwissGroup(SwissGroup group) 
        {
            for (int i = 0; i < group.Players.Count; i++)
            {
                Players.Add(group.Players[i]);
            }

            Score = group.Score;
        }
        public SwissGroup(List<SwissPlayer> players, double score)
        {
            Players = players;
            Score = score;
        }

        public double Score { get; set; }

    }
}
