namespace ChessStatistics.ViewModels.TournamentPage
{
    public class UserPosition
    {
        public UserPosition()
        {
            
        }

        public UserPosition(int tabPosition, int tourPosition)
        {
            TabPosition = tabPosition;
            TourPosition = tourPosition;
        }

        public int TabPosition { get ; set; }

        public int TourPosition { get; set; }  

        public static UserPosition GetDefaultUserPosition()
        {
            return new UserPosition(1, 1);
        }
    }
}
