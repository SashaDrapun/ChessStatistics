using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.ViewModels.PlayersPage;
using ChessStatistics.ViewModels.TournamentsPage;
using System.Collections.Generic;

namespace ChessStatistics.ViewModels.TournamentPage
{
    public class InformationOnPageTournamentModel
    {
        public TournamentOnPageTournamentModel TournamentModel { get; set; }

        public List<PlayerOnPagePlayersModel> PlayersParticipatingInTournament { get; set; }

        public List<PlayerOnPagePlayersModel> PlayersNotParticipatingInTournament { get; set; }

        public TournamentDrawModel TournamentDrawModel { get; set; }

        public RoundRobinTournamentResult RoundRobinTournamentResult { get; set; }

        public UserPosition UserPosition { get; set; }

        public UserModel UserInfo { get; set; }

        public bool IsUserSendRequestToParticipateInTournament { get; set; }

        public List<RequestToParticipateInTournamentModel> RequestsToParticipateInTournamentModels { get; set; }
    }
}
