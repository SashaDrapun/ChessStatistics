using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.ViewModels
{
    public class AddRequestToParticipateInTournamentModel
    {
        public string IdUser{ get; set; }

        public int IdTournament { get; set; }
    }
}