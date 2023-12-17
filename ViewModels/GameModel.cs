using ChessStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.ViewModels
{
    public class GameModel
    {
        public string IdPlayerWhite { get; set; }

        public string IdPlayerBlack { get; set; }

        public GameResult GameResult { get; set; }

        public DateTime Date { get; set; }

    }
}
