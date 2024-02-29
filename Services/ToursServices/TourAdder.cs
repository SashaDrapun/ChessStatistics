using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.ViewModels;
using System.Threading.Tasks;
using System;
using ChessStatistics.Services.TournamentServices;

namespace ChessStatistics.Services.ToursServices
{
    public static class TourAdder
    {
        public static async Task<Tour> AddTourAsync(int tournamentId, int tourNumber)
        {
            Tour tour = new Tour
            {
                IdTournament = tournamentId,
                TourNumber = tourNumber
            };

            Database.db.Tours.Add(tour);
            await Database.db.SaveChangesAsync();

            return tour;
        }

        public static async Task<Tour> AddPlayerSkippingGame(int tourId, int idPlayerSkippingGame)
        {
            Tour tour = TourSearcher.GetTourById(tourId);

            tour.IdPlayerSkippingGame = idPlayerSkippingGame;

            Database.db.Tours.Update(tour);

            await Database.db.SaveChangesAsync();

            return tour;
        }
    }
}
