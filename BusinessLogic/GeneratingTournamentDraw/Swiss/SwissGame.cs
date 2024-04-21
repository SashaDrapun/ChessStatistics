using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessStatistics.Models;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public class SwissGame
    {
        public SwissGame(int idGame, int idPlayerWhite, int idPlayerBlack, int idTour)
        {
            IdGame = idGame;
            IdPlayerWhite = idPlayerWhite;
            IdPlayerBlack = idPlayerBlack;
            IdTour = idTour;
        }

        public SwissGame(int idGame, int idPlayerWhite, int idPlayerBlack, int idTour, GameResult gameResult)
        {
            IdGame = idGame;
            IdPlayerWhite = idPlayerWhite;
            IdPlayerBlack = idPlayerBlack;
            IdTour = idTour;
            GameResult = gameResult;
        }

        public int IdGame { get; set; }

        public int IdPlayerWhite { get; set; }

        public int IdPlayerBlack { get; set; }

        public int IdTour { get; set; }

        public GameResult GameResult { get; set; }

    }
}
