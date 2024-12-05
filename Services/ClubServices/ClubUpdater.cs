using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Threading.Tasks;

namespace ChessStatistics.Services.ClubServices
{
    public static class ClubUpdater
    {
        public static async Task UpdateClubAsync(Club club)
        {
            Database.db.Clubs.Update(club);
            await Database.db.SaveChangesAsync();
        }
    }
}
