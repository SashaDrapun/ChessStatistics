using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.TournamentParticipantsServices;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services
{
    public static class UserSearcher
    {
        public static User GetUserById(string idPlayer)
        {
            return Database.db.Users.FirstOrDefault(u => u.Id == idPlayer);
        }

        public static List<User> GetAllUsers()
        {
            return Database.db.Users.ToList();
        }

        public static User GetUserByEmail(string email)
        {
            return Database.db.Users.FirstOrDefault(user => user.Email == email);
        }

        public static List<User> GetPlayersParticipatingOrNotParticipatingInTournament(int tournamentId, bool isParticipating)
        {
            List<User> players = GetAllUsers();
            List<User> PlayersResult = new List<User>();

            List<string> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipantsByTournamentId(tournamentId);

            foreach (var player in players)
            {
                if (tournamentParticipants.Contains(player.Id) && isParticipating)
                {
                    PlayersResult.Add(GetUserById(player.Id));
                }

                if (!tournamentParticipants.Contains(player.Id) && !isParticipating)
                {
                    PlayersResult.Add(GetUserById(player.Id));
                }
            }

            return PlayersResult;
        }
    }
}
