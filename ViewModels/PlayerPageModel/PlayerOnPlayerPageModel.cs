using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.ViewModels.PlayerPageModel;
using System.Collections.Generic;

namespace ChessStatistics.ViewModels
{
    public class PlayerOnPlayerPageModel
    {
        public int IdPlayer { get; set; }

        public string IdUser {get; set;}

        public string FIO { get; set; }

        public string RankOutput { get; set; }

        public Rating Rating { get; set; }

        public bool IsUserConnectedToPlayer {get; set;}

        public bool IsPlayerConnectedToUser {get; set;}

        public bool IsUserRequestedLinkWithPlayer {get; set;}

        public bool IsPersonalArea {get; set;}
        
        public string PhotoBase64 { get; set; }
        
        public List<GameOnPlayerPageModel> Games { get; set; }
    }
}
