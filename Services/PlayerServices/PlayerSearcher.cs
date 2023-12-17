using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.TournamentParticipantsServices;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services
{
    public static class PlayerSearcher
    {
        public static Player GetPlayer(string idPlayer)
        {
            return Database.db.Users.FirstOrDefault(u => u.Id == idPlayer);
        }

        public static List<Player> GetAllPlayers()
        {
            return Database.db.Users.ToList();
        }

        public static Player GetPlayerByEmail(string email)
        {
            return Database.db.Users.FirstOrDefault(user => user.Email == email);
        }

        public static double GetPlayerRating(string idPlayer)
        {
            return Database.db.Users.FirstOrDefault(u => u.Id == idPlayer).Rating;
        }

        public static List<Player> GetPlayersParticipatingOrNotParticipatingInTournament(int tournamentId, bool isParticipating)
        {
            List<Player> players = GetAllPlayers();
            List<Player> PlayersResult = new List<Player>();

            List<string> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipantsByTournamentId(tournamentId);

            foreach (var player in players)
            {
                if (tournamentParticipants.Contains(player.Id) && isParticipating)
                {
                    PlayersResult.Add(GetPlayer(player.Id));
                }

                if (!tournamentParticipants.Contains(player.Id) && !isParticipating)
                {
                    PlayersResult.Add(GetPlayer(player.Id));
                }
            }

            return PlayersResult;
        }
    }
}
