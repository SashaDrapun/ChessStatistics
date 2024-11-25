using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.ViewModels;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class PlayerAdder
    {
        public static async Task<Player> AddPlayerAsync(PlayerModel playerModel)
        {
            Player player = new Player
            {
                FIO = playerModel.FIO,
                Rank = playerModel.Rank,
                RatingBlitz = playerModel.CurrentRating,
                RatingRapid = playerModel.CurrentRating,
                RatingClassic = playerModel.CurrentRating,
                Photo = playerModel.Photo
            };

            Database.db.Players.Add(player);
            await Database.db.SaveChangesAsync();
            return player;
        }
    }
}
