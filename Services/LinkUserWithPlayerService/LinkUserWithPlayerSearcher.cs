using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services.LinkUserWithPlayerService
{
    public static class LinkUserWithPlayerSearcher
    {
        private static LinkUserWithPlayer SetLinkUserWithPlayers(LinkUserWithPlayer linkUserWithPlayer)
        {
            linkUserWithPlayer.Player = PlayerSearcher.GetPlayerById(linkUserWithPlayer.IdPlayer);
            linkUserWithPlayer.User = UserSearcher.GetUserById(linkUserWithPlayer.IdUser);
            return linkUserWithPlayer;
        }

        public static LinkUserWithPlayer GetLinkById(int idLink)
        {
            return SetLinkUserWithPlayers(Database.db.LinksUser.FirstOrDefault(link => link.Id == idLink));
        }

        public static List<LinkUserWithPlayer> GetLinksUserWithPlayers()
        {
            List<LinkUserWithPlayer> linkUserWithPlayers = Database.db.LinksUser.ToList();

            for (int i = 0; i < linkUserWithPlayers.Count; i++) 
            {
                linkUserWithPlayers[i] = SetLinkUserWithPlayers(linkUserWithPlayers[i]);
            }

            return linkUserWithPlayers;
        }

        public static bool IsUserRequestedLinkWithPlayer(User user)
        {
            return Database.db.LinksUser.FirstOrDefault(link => link.IdUser == user.Id) != null;
        }
    }
}
