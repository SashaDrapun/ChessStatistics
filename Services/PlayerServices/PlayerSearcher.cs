using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class PlayerSearcher
    {
        public static Player GetPlayerById(int idPlayer)
        {
            return Database.db.Players.FirstOrDefault(player => player.Id == idPlayer);
        }

        public static double GetPlayerRating(int idPlayer)
        {
            return Database.db.Players.FirstOrDefault(player => player.Id == idPlayer).Rating;
        }
    }
}
