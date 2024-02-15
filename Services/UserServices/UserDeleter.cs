using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Threading.Tasks;

namespace ChessStatistics.Services.UserServices
{
    public static class UserDeleter
    {
        public static async Task DeleteUser(string idUser)
        {
            User user = UserSearcher.GetUserById(idUser);
            Database.db.Remove(user);
            await Database.db.SaveChangesAsync();
        }
    }
}
