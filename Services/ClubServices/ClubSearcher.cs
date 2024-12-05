using ChessStatistics.BusinessLogic;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services.ClubServices
{
    public static class ClubSearcher
    {
        public static List<Club> GetClubs()
        {
            return Database.db.Clubs.ToList();
        }

        public static Club GetClubById(int idClub)
        {
            return Database.db.Clubs.FirstOrDefault(club => club.Id == idClub);
        }
    }
}
