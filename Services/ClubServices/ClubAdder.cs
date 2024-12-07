using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.ViewModels.ClubPage;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ChessStatistics.Services.ClubServices
{
    public static class ClubAdder
    {
        public static async Task<Club> AddClubAsync(ClubModel clubModel)
        {
            Club club = new Club
            {
                Name = clubModel.Name,
                Description = clubModel.Description,
                Photo = clubModel.Photo
            };

            Database.db.Clubs.Add(club);
            await Database.db.SaveChangesAsync();
            return club;
        }
    }
}
