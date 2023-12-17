using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class PlayerUpdater
    {
        public static async Task UpdatePlayerAsync(Player player)
        {
            Database.db.Users.Update(player);
            await Database.db.SaveChangesAsync();
        }

        public static async Task UpdateRatingPlayerAsync(string idPlayer, double newRating)
        {
            Player player = PlayerSearcher.GetPlayer(idPlayer);
            player.Rating = newRating;
            await UpdatePlayerAsync(player);
        }
    }
}
