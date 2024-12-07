using ChessStatistics.ViewModels.ClubPage;
using System.Collections.Generic;

namespace ChessStatistics.Mappers
{
    public static class ClubMapper
    {

        public static List<ClubModel> MapClubs(List<Club> clubs)
        {
            var result = new List<ClubModel>();
            foreach (var club in clubs) 
            {
                result.Add(MapClub(club));
            }

            return result;
        }

        public static ClubModel MapClub(Club club)
        {
            var result = new ClubModel();
            result.Id = club.Id;
            result.Name = club.Name;
            result.Description = club.Description;
            result.Photo = club.Photo;
            return result;
        }
    }
}
