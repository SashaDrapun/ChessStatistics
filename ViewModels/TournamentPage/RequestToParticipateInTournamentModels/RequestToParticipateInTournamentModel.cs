using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.ViewModels.TournamentPage
{
    public class RequestToParticipateInTournamentModel
    {
        public string IdUser { get; set; }

        public int IdTournament { get; set; }

        public string PlayerFIO { get; set; }

        public string PlayerRank { get; set; }

        public double PlayerRating { get; set; }
    }
}