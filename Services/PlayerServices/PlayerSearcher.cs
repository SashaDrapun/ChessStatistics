using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.TournamentParticipantsServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.PlayerServices
{
    public static class PlayerSearcher
    {
        public static Player GetPlayerById(int idPlayer)
        {
            return Database.db.Players.FirstOrDefault(player => player.IdPlayer == idPlayer);
        }

        public static double GetPlayerScore(int idPlayerSkippingGame, int idTour)
        {
            double scorePlayer = 0;
            Tour tour = TourSearcher.GetTourById(idTour);
            List<Tour> tours = TourSearcher.GetToursByTournament(tour.IdTournament);

            for (int i = 0; i < tour.TourNumber - 1; i++)
            {
                List<Game> games = GameSearcher.GetGamesByTour(tours[i].IdTour);

                foreach (var currentGame in games)
                {
                    if (!currentGame.DidTheGamePassed)
                    {
                        continue;
                    }

                    if (currentGame.IdPlayerWhite == idPlayerSkippingGame)
                    {
                        if (currentGame.GameResult == GameResult.WhiteWin)
                        {
                            scorePlayer++;
                        }

                        if (currentGame.GameResult == GameResult.Draw)
                        {
                            scorePlayer += 0.5;
                        }
                    }

                    if (currentGame.IdPlayerBlack == idPlayerSkippingGame)
                    {
                        if (currentGame.GameResult == GameResult.BlackWin)
                        {
                            scorePlayer++;
                        }

                        if (currentGame.GameResult == GameResult.Draw)
                        {
                            scorePlayer += 0.5;
                        }
                    }
                }

                if (tours[i].IdPlayerSkippingGame == idPlayerSkippingGame)
                {
                    scorePlayer++;
                }

                if (tours[i].IdPlayerSkippingGame == idPlayerSkippingGame)
                {
                    scorePlayer++;
                }
            }

            return scorePlayer;
        }

        public static List<Player> GetAllPlayers()
        {
            return [.. Database.db.Players]; 
        }

        public static double GetPlayerRating(int idPlayer, RatingType ratingType)
        {
            Player player = Database.db.Players.FirstOrDefault(player => player.IdPlayer == idPlayer) ?? throw new NullReferenceException();


            if (ratingType == RatingType.Blitz)
            {
                return player.RatingBlitz;
            }

            if (ratingType == RatingType.Rapid)
            {
                return player.RatingRapid;
            }

            return player.RatingClassic;

        }

        public static List<Player> GetPlayersNotLinkedWithUser()
        {
            List<Player> PlayersNotLinkedWithUser = [];

            foreach (var player in Database.db.Players)
            {
                if (player.IdUser == null)
                {
                    PlayersNotLinkedWithUser.Add(player);
                }
            }

            return PlayersNotLinkedWithUser;
        }

        public static List<Player> GetPlayersParticipatingOrNotParticipatingInTournament(int tournamentId, bool isParticipating)
        {
            List<Player> players = GetAllPlayers();
            List<Player> PlayersResult = [];

            List<int> tournamentParticipants = TournamentParticipantsSearcher.GetTournamentParticipantsIdPlayerByTournamentId(tournamentId);

            foreach (var player in players)
            {
                if (tournamentParticipants.Contains(player.IdPlayer) && isParticipating)
                {
                    PlayersResult.Add(GetPlayerById(player.IdPlayer));
                }

                if (!tournamentParticipants.Contains(player.IdPlayer) && !isParticipating)
                {
                    PlayersResult.Add(GetPlayerById(player.IdPlayer));
                }
            }

            return PlayersResult;
        }
    }
}
