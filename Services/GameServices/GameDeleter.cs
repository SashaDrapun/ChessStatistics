using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.ToursServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessStatistics.Services.GameServices
{
    public static class GameDeleter
    {
        public static async Task DeleteGames(int idTour)
        {
            List<Game> games = GameSearcher.GetGamesByTour(idTour);

            foreach (var game in games)
            {
                Database.db.Remove(game);
            }

            await Database.db.SaveChangesAsync();
        }
    }
}
