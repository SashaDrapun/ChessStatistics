using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public class PlayerUpdater
    {
        public static async Task UpdatePlayerAsync(Player player)
        {
            Database.db.Players.Update(player);
            await Database.db.SaveChangesAsync();
        }

        public static async Task UpdateRatingPlayerAsync(int idPlayer, double newRating)
        {
            Player player = PlayerSearcher.GetPlayerById(idPlayer);
            player.Rating = newRating;
            await UpdatePlayerAsync(player);
        }
    }
}
