using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.LinkUserWithPlayerService;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class UserUpdater
    {
        public static async Task UpdateUserAsync(User user)
        {
            Database.db.Users.Update(user);
            await Database.db.SaveChangesAsync();
        }

        public static async Task AppointAdministrator(string idUser)
        {
            User user = UserSearcher.GetUserById(idUser);
            user.IsAdmin = true;
            await Database.db.SaveChangesAsync();
        }
        public static async Task SetPlayer(int idLink)
        {
            LinkUserWithPlayer linkUserWithPlayer = LinkUserWithPlayerSearcher.GetLinkById(idLink);

            User user = UserSearcher.GetUserById(linkUserWithPlayer.IdUser);
            Player player = PlayerSearcher.GetPlayerById(linkUserWithPlayer.IdPlayer);

            user.IdPlayer = player.Id;
            player.IdUser = user.Id;
            await Database.db.SaveChangesAsync();
        }
    }
}
