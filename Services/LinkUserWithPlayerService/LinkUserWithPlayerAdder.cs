using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.ViewModels.PlayerPageModel;
using System.Threading.Tasks;

namespace ChessStatistics.Services.LinkUserWithPlayerService
{
    public static class LinkUserWithPlayerAdder
    {
        public static async Task<LinkUserWithPlayer> AddLink(LinkUserWithPlayerModel linkUserWithPlayerModel)
        {
            LinkUserWithPlayer linkUserWithPlayer = new LinkUserWithPlayer
            {
                IdPlayer = linkUserWithPlayerModel.IdPlayer,
                IdUser = linkUserWithPlayerModel.IdUser
            };

            Database.db.LinksUser.Add(linkUserWithPlayer);
            await Database.db.SaveChangesAsync();

            return linkUserWithPlayer;
        }
    }
}
