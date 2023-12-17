using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Linq;


namespace ChessStatistics.Services.TournamentServices
{
    public class TournamentSearcher
    {
        public static Tournament GetTournamentById(int IdTournament)
        {
            return Database.db.Tournaments.FirstOrDefault(t => t.Id == IdTournament);
        }
    }
}
