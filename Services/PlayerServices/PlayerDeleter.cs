using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class PlayerDeleter
    {
        public static async Task DeletePlayer(int idPlayer)
        {
            Player player = PlayerSearcher.GetPlayerById(idPlayer);
            Database.db.Remove(player);
            await Database.db.SaveChangesAsync();
        }
    }
}
