using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using System.Collections.Generic;
using System.Linq;

namespace ChessStatistics.Services.GameServices
{
    public static class GameSearcher
    {
        private static Game SetGame(Game game)
        {
            game.PlayerWhite = PlayerSearcher.GetPlayerById(game.IdPlayerWhite);
            game.PlayerBlack = PlayerSearcher.GetPlayerById(game.IdPlayerBlack);

            return game;
        }

        private static List<Game> SetGames(List<Game> games)
        {
            for (int i = 0; i < games.Count; i++)
            {
                games[i] = SetGame(games[i]);
            }

            return games;
        }

        public static List<Game> GetGamesByTour(int tourId) 
        {
            return Database.db.Games.Where(game => game.IdTour == tourId).ToList();
        }

        public static Game GetGame(int idGame)
        {
            return SetGame(Database.db.Games.FirstOrDefault(g => g.Id == idGame));
        }

        public static List<Game> GetAllGames()
        {
            return SetGames(Database.db.Games.ToList());
        }
    }
}
