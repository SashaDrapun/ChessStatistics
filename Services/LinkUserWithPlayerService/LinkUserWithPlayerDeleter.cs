using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using System.Threading.Tasks;

namespace ChessStatistics.Services.LinkUserWithPlayerService
{
    public static class LinkUserWithPlayerDeleter
    {
        public static async Task DeleteLinkUser(int idLink)
        {
            LinkUserWithPlayer linkUserWithPlayer = LinkUserWithPlayerSearcher.GetLinkById(idLink);
            Database.db.Remove(linkUserWithPlayer);
            await Database.db.SaveChangesAsync();
        }
    }
}
