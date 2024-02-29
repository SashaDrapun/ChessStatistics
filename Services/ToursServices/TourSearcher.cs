using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.ToursServices
{
    public static class TourSearcher
    {
        public static Tour GetTourById(int idTour)
        {
            return Database.db.Tours.FirstOrDefault(t => t.Id == idTour);
        }

        public static List<Tour> GetToursByTournament(int idTournament)
        {
            return Database.db.Tours.Where(t => t.IdTournament == idTournament).ToList();
        }
    }
}
