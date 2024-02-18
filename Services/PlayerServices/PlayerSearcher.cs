using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Collections.Generic;
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

        public static List<Player> GetPlayersNotLinkedWithUser()
        {
            List<Player> PlayersNotLinkedWithUser = new List<Player>();

            foreach (var player in Database.db.Players)
            {
                if (player.IdUser == null)
                {
                    PlayersNotLinkedWithUser.Add(player);
                }
            }

            return PlayersNotLinkedWithUser;
        }
    }
}
