using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessStatistics.Services.ToursServices
{
    public static class TourDeleter
    {
        public static async Task DeleteTours(int idTournament)
        {
            List<Tour> tours = TourSearcher.GetToursByTournament(idTournament);

            foreach (var tour in tours)
            {
                await GameDeleter.DeleteGames(tour.IdTour);
                Database.db.Remove(tour);
            }

            await Database.db.SaveChangesAsync();
        }
    }
}
