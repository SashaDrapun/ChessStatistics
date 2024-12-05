using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using System.Threading.Tasks;

namespace ChessStatistics.Services.ClubServices
{
    public static class ClubDeleter
    {
        public static async Task DeleteClub(int idClub)
        {
            Club club = ClubSearcher.GetClubById(idClub);
            Database.db.Remove(club);
            await Database.db.SaveChangesAsync();
        }
    }
}
