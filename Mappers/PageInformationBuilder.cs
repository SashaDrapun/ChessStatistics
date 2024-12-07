using ChessStatistics.BusinessLogic;
using ChessStatistics.ViewModels.TournamentsPage;
using System.Linq;

namespace ChessStatistics.Mappers
{
    public static class PageInformationBuilder
    {
        public static InformationOnPageTournamentsModel Tournaments()
        {
            var result = new InformationOnPageTournamentsModel();

            result.TournamentModels = TournamentMapper.
                MapTournamentsToTournamentsOnPageTournamentsModel(Database.db.Tournaments.ToList());

            return result;
        }
    }
}
