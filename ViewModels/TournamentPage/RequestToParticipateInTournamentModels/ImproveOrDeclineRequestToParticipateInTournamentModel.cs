namespace ChessStatistics.ViewModels.TournamentPage.RequestToParticipateInTournamentModels
{
    public enum DeclineOrImprove
    {
        Decline,
        Improve
    }
    public class ImproveOrDeclineRequestToParticipateInTournamentModel
    {
        public string IdUser { get; set; }

        public int IdTournament { get; set; }

        public DeclineOrImprove DeclineOrImprove { get; set; }
    }
}
