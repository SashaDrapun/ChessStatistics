using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.ToursServices;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services.GameServices
{
    public static class GameSearcher
    {
        public static List<Game> GetGamesByTour(int tourId)
        {
            return Database.db.Games.Where(game => game.IdTour == tourId).ToList();
        }

        public static List<Game> GetGamesByTournament(int tournamentId)
        {
            Tournament tournament = Database.db.Tournaments.FirstOrDefault(t => t.IdTournament == tournamentId);

            List<Tour> tours = TourSearcher.GetToursByTournament(tournamentId);

            List<Game> games = new List<Game>();

            for (int i = 0; i < tours.Count; i++)
            {
                games.AddRange(GetGamesByTour(tours[i].IdTour));
            }

            return games;
        }

        public static List<Game> GetGamesByPlayer(int playerId)
        {
            return Database.db.Games.Where(game => (game.IdPlayerWhite == playerId) || (game.IdPlayerBlack == playerId)).ToList();
        }

        public static Game GetGame(int idGame)
        {
            return Database.db.Games.FirstOrDefault(g => g.IdGame == idGame);
        }

        public static List<Game> GetAllGames()
        {
            return Database.db.Games.ToList();
        }
    }
}
