using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw
{
    internal class PlayerInGeneratingDraw
    {
        internal PlayerInGeneratingDraw(int playerNumber)
        {
            PlayerId = playerNumber;
            Tours = new List<PlayerTour>(); 
        }
        internal int PlayerId { get; set; }

        internal List<PlayerTour> Tours { get; set; }
    }
}
