using ChessStatistics.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.ViewModels
{
    public class LinkUserWithPlayerModel
    {
        public int Id { get; set; }

        public string IdUser { get; set; }

        public int IdPlayer { get; set; }
    }
}
