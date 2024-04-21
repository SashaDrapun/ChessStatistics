using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public class SwissTour
    {
        public SwissTour(int idTour, int tourNumber)
        {
            IdTour = idTour;
            TourNumber = tourNumber;
            Games = new List<SwissGame>();
        }

        public SwissTour(int idTour, int tourNumber, int idPlayerSkippingGame)
        {
            IdTour = idTour;
            TourNumber = tourNumber;
            IdPlayerSkippingGame = idPlayerSkippingGame;
            Games = new List<SwissGame>();
        }

        public int IdTour { get; set; }

        public int TourNumber { get; set; }

        public int IdPlayerSkippingGame { get; set; }

        public List<SwissGame> Games { get; set; }

    }
}
