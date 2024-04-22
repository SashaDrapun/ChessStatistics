using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Round
{
    public static class GeneratingRoundTournamentDraw
    {
        public static async Task GenerateTournamentDrawAsync(int idTournament)
        {
            List<Player> playersParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(idTournament, true);

            int numberOfParticipants = playersParticipatingInTournament.Count;

            List<PlayerInGeneratingDraw> players = new List<PlayerInGeneratingDraw>();

            for (int i = 0; i < playersParticipatingInTournament.Count; i++)
            {
                players.Add(new PlayerInGeneratingDraw(playersParticipatingInTournament[i].IdPlayer));
            }

            if (numberOfParticipants % 2 != 0)
            {
                players.Add(new PlayerInGeneratingDraw(-1));
                numberOfParticipants++;
            }

            int lastEvenNumber = numberOfParticipants % 2 == 0 ? numberOfParticipants : numberOfParticipants - 1;

            for (int playerNumber = 1; playerNumber < numberOfParticipants; playerNumber++)
            {
                for (int enemyNumber = playerNumber + 1; enemyNumber <= numberOfParticipants; enemyNumber++)
                {
                    int tourNumber;
                    Color colorPlayer = Color.Black;
                    Color colorEnemyPlayer = Color.White;
                    if (enemyNumber != lastEvenNumber)
                    {
                        int sumNumber = playerNumber + enemyNumber;
                        tourNumber = GetTourNumberForUsualPlayer(lastEvenNumber, sumNumber);
                        SetColorForUsualPlayer(playerNumber, enemyNumber, sumNumber, ref colorPlayer, ref colorEnemyPlayer);
                    }
                    else
                    {
                        int sumNumber = playerNumber * 2;
                        tourNumber = GetTourNumberForLastEvenPlayer(numberOfParticipants, sumNumber);
                        SetColorForLastEvenPlayer(numberOfParticipants, playerNumber, ref colorPlayer, ref colorEnemyPlayer);
                    }

                    players[playerNumber - 1].Tours.Add(new PlayerTour(tourNumber, colorPlayer, enemyNumber));
                    players[enemyNumber - 1].Tours.Add(new PlayerTour(tourNumber, colorEnemyPlayer, playerNumber));
                }
            }

            for (int tourNumber = 1; tourNumber < numberOfParticipants; tourNumber++)
            {
                Tour tour = await TourAdder.AddTourAsync(idTournament, tourNumber);
                List<int> written = new List<int>();
                for (int i = 0; i < players.Count; i++)
                {
                    if (written.Contains(i + 1))
                    {
                        continue;
                    }

                    PlayerTour playerTour = players[i].Tours.Where(t => t.TourNumber == tourNumber).FirstOrDefault();

                    if (players[playerTour.EnemyNumber - 1].PlayerId == -1)
                    {
                        await TourAdder.AddPlayerSkippingGame(tour.IdTour, players[i].PlayerId);
                        written.Add(playerTour.EnemyNumber);
                        continue;
                    }

                    if (playerTour.Color == Color.White)
                    {
                        Game game = new Game
                        {
                            IdPlayerWhite = players[i].PlayerId,
                            IdPlayerBlack = players[playerTour.EnemyNumber - 1].PlayerId,
                            IdTour = tour.IdTour
                        };
                        await GameAdder.AddNotPassedGameAsync(GameMapper.MapGame(game));
                    }
                    else
                    {
                        Game game = new Game
                        {
                            IdPlayerWhite = players[playerTour.EnemyNumber - 1].PlayerId,
                            IdPlayerBlack = players[i].PlayerId,
                            IdTour = tour.IdTour
                        };

                        await GameAdder.AddNotPassedGameAsync(GameMapper.MapGame(game));
                    }

                    written.Add(playerTour.EnemyNumber);
                }
            }
        }

        private static void SetColorForLastEvenPlayer(int numberOfParticipants, int playerNumber, ref Color colorPlayer, ref Color colorEnemyPlayer)
        {
            if (playerNumber <= numberOfParticipants / 2)
            {
                colorPlayer = Color.White;
                colorEnemyPlayer = Color.Black;
            }
        }

        private static void SetColorForUsualPlayer(int playerNumber, int enemyNumber, int sumNumber, ref Color colorPlayer, ref Color colorEnemyPlayer)
        {
            if (sumNumber % 2 == 0)
            {
                if (playerNumber >= enemyNumber)
                {
                    colorPlayer = Color.White;
                    colorEnemyPlayer = Color.Black;
                }
                else
                {
                    colorPlayer = Color.Black;
                    colorEnemyPlayer = Color.White;
                }
            }
            else
            {
                if (playerNumber >= enemyNumber)
                {
                    colorPlayer = Color.Black;
                    colorEnemyPlayer = Color.White;
                }
                else
                {
                    colorPlayer = Color.White;
                    colorEnemyPlayer = Color.Black;
                }
            }
        }

        private static int GetTourNumberForLastEvenPlayer(int numberOfParticipants, int tourNumber)
        {
            if (tourNumber <= numberOfParticipants)
            {
                tourNumber--;
            }
            else
            {
                tourNumber -= numberOfParticipants;
            }

            return tourNumber;
        }

        private static int GetTourNumberForUsualPlayer(int lastEvenNumber, int sumNumber)
        {
            if (sumNumber <= lastEvenNumber)
            {
                return sumNumber - 1;
            }
            else
            {
                return sumNumber - lastEvenNumber;
            }
        }
    }
}
