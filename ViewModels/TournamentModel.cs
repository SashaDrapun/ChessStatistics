using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Models;
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

        public List<Player> PlayersParticipatingInTournament { get; set; }

        public List<Player> PlayersNotParticipatingInTournament { get; set; }

        public TournamentDrawModel TournamentDrawModel { get; set; }

        public RoundRobinTournamentResult RoundRobinTournamentResult { get; set; }
    }
}
