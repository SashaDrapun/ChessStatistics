using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.TournamentParticipantsServices;
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

        public static List<Player> GetAllPlayers()
        {
            return Database.db.Players.ToList(); 
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

        public static List<Player> GetPlayersParticipatingOrNotParticipatingInTournament(int tournamentId, bool isParticipating)
        {
            List<Player> players = GetAllPlayers();
            List<Player> PlayersResult = new List<Player>();

            List<int> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipantsByTournamentId(tournamentId);

            foreach (var player in players)
            {
                if (tournamentParticipants.Contains(player.Id) && isParticipating)
                {
                    PlayersResult.Add(GetPlayerById(player.Id));
                }

                if (!tournamentParticipants.Contains(player.Id) && !isParticipating)
                {
                    PlayersResult.Add(GetPlayerById(player.Id));
                }
            }

            return PlayersResult;
        }
    }
}
