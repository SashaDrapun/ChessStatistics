using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw
{
    public class PlayerTour
    {
        public PlayerTour(int tourNumber, Color color, int enemyNumber)
        {
            TourNumber = tourNumber;
            Color = color;
            EnemyNumber = enemyNumber;
        }

        public int TourNumber { get ; set; }

        public Color Color { get; set; }

        public int EnemyNumber {  get; set; }
    }
}
