using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services
{
    public static class UserSearcher
    {
        private static User SetUser(User user)
        {
            if (user == null) 
            {
                return null;
            }

            if (user.IdPlayer != 0)
            {
                user.Player = PlayerSearcher.GetPlayerById(user.IdPlayer);
            }

            return user;
        }

        private static List<User> SetUsers(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                users[i] = SetUser(users[i]);
            }

            return users;
        }

        public static bool IsUserConnectedToThePlayer(User user)
        {
            return user.IdPlayer != 0;
        }

        public static User GetUserById(string idUser)
        {
            return SetUser(Database.db.Users.FirstOrDefault(u => u.Id == idUser));
        }

        public static List<User> GetAllUsers()
        {
            return SetUsers(Database.db.Users.ToList());
        }

        public static User GetUserByEmail(string email)
        {
            return SetUser(Database.db.Users.FirstOrDefault(user => user.Email == email));
        }
    }
}
