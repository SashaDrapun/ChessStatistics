using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
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

        public static async Task UpdateRatingPlayerAsync(int idPlayer, double newRating, RatingType ratingType)
        {
            Player player = PlayerSearcher.GetPlayerById(idPlayer);

            if (ratingType == RatingType.Blitz)
            {
                player.RatingBlitz = newRating;
            }

            if (ratingType == RatingType.Rapid)
            {
                player.RatingRapid = newRating;
            }

            if (ratingType == RatingType.Classic)
            {
                player.RatingClassic = newRating;
            }

            await UpdatePlayerAsync(player);
        }
    }
}
