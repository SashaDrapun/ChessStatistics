﻿using ChessStatistics.Models;
using System;
using System.Collections.Generic;


namespace ChessStatistics.ViewModels
{
    public class TournamentModel
    {
        public int IdTournament { get; set; }
        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }

        public string Type { get; set; }

        public int CountTours { get; set; }

        public TournamentDrawModel TournamentDrawModel { get; set; }
    }
}
