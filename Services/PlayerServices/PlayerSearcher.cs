using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.TournamentParticipantsServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class PlayerSearcher
    {
        public static Player GetPlayerById(int idPlayer)
        {
            return Database.db.Players.FirstOrDefault(player => player.IdPlayer == idPlayer);
        }

        public static List<Player> GetAllPlayers()
        {
            return Database.db.Players.ToList(); 
        }

        public static double GetPlayerRating(int idPlayer, RatingType ratingType)
        {
            Player player = Database.db.Players.FirstOrDefault(player => player.IdPlayer == idPlayer) ?? throw new NullReferenceException();


            if (ratingType == RatingType.Blitz)
            {
                return player.RatingBlitz;
            }

            if (ratingType == RatingType.Rapid)
            {
                return player.RatingRapid;
            }

            return player.RatingClassic;

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

            List<int> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipantsIdPlayerByTournamentId(tournamentId);

            foreach (var player in players)
            {
                if (tournamentParticipants.Contains(player.IdPlayer) && isParticipating)
                {
                    PlayersResult.Add(GetPlayerById(player.IdPlayer));
                }

                if (!tournamentParticipants.Contains(player.IdPlayer) && !isParticipating)
                {
                    PlayersResult.Add(GetPlayerById(player.IdPlayer));
                }
            }

            return PlayersResult;
        }
    }
}
