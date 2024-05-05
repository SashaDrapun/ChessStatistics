using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using ChessStatistics.Models;

namespace ChessStatistics.ViewModels
{
    public class UserModel
    {
        public string IdUser { get; set; }
        [ForeignKey("Player")]
        public int IdPlayer { get; set; }

        public bool IsAdmin { get; set; }

        public string FIO { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime DateRegistration { get; set; }

        public DateTime DateLastLogin { get; set; }

        public List<GameModel> Games { get; set; }

        public bool IsUserConnectedToThePlayer { get; set; }

        public PlayerModel Player { get; set; }
    }
}
