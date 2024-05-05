using ChessStatistics.Models.Enum;
using System.Collections.Generic;

namespace ChessStatistics.ViewModels
{
    public class PlayerOnPlayerPageModel
    {
        public int IdPlayer { get; set; }

        public string FIO { get; set; }

        public Rank Rank { get; set; }

        public string RankOutput { get; set; }

        public Rating Rating { get; set; }

        public List<GameModel> Games { get; set; }
    }
}
