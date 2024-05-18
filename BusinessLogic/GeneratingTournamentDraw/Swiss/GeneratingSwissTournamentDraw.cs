using ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Round;
using ChessStatistics.Mappers;
using ChessStatistics.Models;
using ChessStatistics.Services;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public static class GeneratingSwissTournamentDraw
    {
        public static Random random = new Random();
        public static List<SwissPair> pairsPlayed = new List<SwissPair>();

        public static async Task AddNextTourAsync(int idTournament)
        {
            TournamentModel model = TournamentSearcher.GetTournamentModelById(idTournament);

            List<SwissPlayer> swissPlayers = SetSwissPlayersAndPairsPlayed(model);

            SwissTour swissTour;
            int tourNumber = model.TournamentDrawModel.Tours.Count == 0 ? 1 : model.TournamentDrawModel.Tours.Max(x => x.TourNumber) + 1;
            Tour tour = await TourAdder.AddTourAsync(idTournament, tourNumber);

            if (model.TournamentDrawModel.Tours.Count == 0)
            {
                swissTour = DrawFirstRound(swissPlayers);
            }
            else
            {
                swissTour = GetNextTour(swissPlayers);
            }

            foreach(var tourGame in swissTour.Games)
            {
                Game game = new Game
                {
                    IdPlayerWhite = tourGame.IdPlayerWhite,
                    IdPlayerBlack = tourGame.IdPlayerBlack,
                    IdTour = tour.IdTour
                };
                await GameAdder.AddNotPassedGameAsync(GameMapper.MapGame(game));
            }

            if (swissTour.IdPlayerSkippingGame > 0)
            {
                await TourAdder.AddPlayerSkippingGame(tour.IdTour, swissTour.IdPlayerSkippingGame);
            }
        }

        private static List<SwissPlayer> SetSwissPlayersAndPairsPlayed(TournamentModel model)
        {
            List<SwissPlayer> swissPlayers = new List<SwissPlayer>();
            foreach (var player in model.PlayersParticipatingInTournament)
            {
                Rating rating = new Rating(player.RatingBlitz, player.RatingRapid, player.RatingClassic, model.RatingType);
                swissPlayers.Add(new SwissPlayer(player.IdPlayer, player.FIO, RatingOperations.GetRating(rating), 0));
            }

            foreach (var tour in model.TournamentDrawModel.Tours)
            {
                foreach (var game in tour.Games)
                {
                    swissPlayers.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().CountGamePlayingWithWhite++;
                    swissPlayers.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().CountGamePlayingWithBlack++;

                    if (game.GameResult == GameResult.WhiteWin)
                    {
                        swissPlayers.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().Score++;
                    }

                    if (game.GameResult == GameResult.Draw)
                    {
                        swissPlayers.Where(player => player.Id == game.IdPlayerWhite).FirstOrDefault().Score += 0.5;
                        swissPlayers.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().Score += 0.5;
                    }

                    if (game.GameResult == GameResult.BlackWin)
                    {
                        swissPlayers.Where(player => player.Id == game.IdPlayerBlack).FirstOrDefault().Score += 1;
                    }

                    pairsPlayed.Add(new SwissPair(game.IdPlayerWhite, game.IdPlayerBlack));
                }

                if (tour.IdPlayerSkippingGame > 0)
                {
                    swissPlayers.Where(player => player.Id == tour.IdPlayerSkippingGame).FirstOrDefault().DidMissTheTour = true;
                }
            }

            return swissPlayers;
        }

        private static SwissTour GetNextTour(List<SwissPlayer> players)
        {
            SwissTour tour = new SwissTour(1, 1);

            SwissPlayer playerSkippingGame = null;
            int idPlayerSkippingGame = -1;
            if (players.Count % 2 != 0)
            {
                for (int i = players.Count - 1; i >= 0; i--)
                {
                    if (!players[i].DidMissTheTour)
                    {
                        idPlayerSkippingGame = players[i].Id;
                        players[i].Score++;
                        players[i].DidMissTheTour = true;
                        playerSkippingGame = players[i];
                        players.Remove(playerSkippingGame);
                        break;
                    }
                }
            }

            tour.IdPlayerSkippingGame = idPlayerSkippingGame;

            List<SwissGroup> groups = SetGroups(players);

            int gameNumber = 1;
            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].Players = groups[i].Players.OrderByDescending(p => p.Score)
                                             .ThenByDescending(p => p.Rating)
                                             .ToList();
                List<SwissPair> unsuitablePairsForThisLevel = new List<SwissPair>();

                while (groups[i].Players.Count > 1) // пока в группе более 1 пользователя, мы ищем игроку соперника в его очковой группе. 
                {

                    int halfCount = groups[i].Players.Count / 2;
                    int index = halfCount;

                    SwissPair pair;
                    SwissPlayer firstPlayer = groups[i].Players[0];
                    SwissPlayer secondPlayer;
                    bool isAllPlayersChecked = false;
                    bool isPairFind = true;
                    do
                    {
                        if (index == groups[i].Players.Count)
                        {
                            index = 1;
                        }

                        secondPlayer = groups[i].Players[index];
                        pair = new SwissPair(firstPlayer.Id, secondPlayer.Id);

                        if (index == halfCount)
                        {
                            if (isAllPlayersChecked)
                            {

                                groups[i + 1].Players.Add(firstPlayer);
                                groups[i].Players.Remove(firstPlayer);
                                isPairFind = false;
                                break; // перенос игрока в следующую группу, если соперник не найден
                            }
                            else
                            {
                                isAllPlayersChecked = true;
                            }
                        }

                        index++;
                    }
                    while ((pairsPlayed.Contains(pair)) || (unsuitablePairsForThisLevel.Contains(pair)));

                    if (isPairFind)
                    {
                        List<SwissGroup> groupForTest = Cloner.CloneGroups(groups);

                        groupForTest[i].Players.Remove(firstPlayer);
                        groupForTest[i].Players.Remove(secondPlayer);

                        SwissPair pair1 = new SwissPair(firstPlayer.Id, secondPlayer.Id);
                        SwissPair pair2 = new SwissPair(secondPlayer.Id, firstPlayer.Id);


                        if (IsItPossibleToGenerateDraw(groupForTest))
                        {
                            AddGame(tour, gameNumber++, firstPlayer, secondPlayer);
                            groups[i].Players.Remove(firstPlayer);
                            groups[i].Players.Remove(secondPlayer);
                            pairsPlayed.Add(pair1);
                            pairsPlayed.Add(pair2);
                        }
                        else
                        {
                            unsuitablePairsForThisLevel.Add(pair1);
                            unsuitablePairsForThisLevel.Add(pair2);
                        }
                    }
                }

                if (groups[i].Players.Count == 1)
                {
                    groups[i + 1].Players.Add(groups[i].Players[0]);
                    groups[i].Players.Remove(groups[i].Players[0]);
                }
            }

            if (playerSkippingGame != null)
            {
                players.Add(playerSkippingGame);
            }

            return tour;
        }

        private static bool IsItPossibleToGenerateDraw(List<SwissGroup> testGroups)
        {
            for (int i = 0; i < testGroups.Count; i++)
            {
                testGroups[i].Players = testGroups[i].Players.OrderByDescending(p => p.Score)
                                             .ThenByDescending(p => p.Rating)
                                             .ToList();

                List<SwissPair> unsuitablePairsForThisLevel = new List<SwissPair>();
                while (testGroups[i].Players.Count > 1) // пока в группе более 1 пользователя, мы ищем игроку соперника в его очковой группе. 
                {
                    int halfCount = testGroups[i].Players.Count / 2;
                    int index = halfCount;

                    SwissPair pair;
                    SwissPlayer firstPlayer = testGroups[i].Players[0];
                    SwissPlayer secondPlayer;
                    bool isAllPlayersChecked = false;
                    bool isPairFind = true;
                    do
                    {
                        if (index == testGroups[i].Players.Count)
                        {
                            index = 1;
                        }

                        secondPlayer = testGroups[i].Players[index];
                        pair = new SwissPair(firstPlayer.Id, secondPlayer.Id);

                        if (index == halfCount)
                        {
                            if (isAllPlayersChecked)
                            {
                                if (i == testGroups.Count - 1)
                                {
                                    return false;
                                }
                                else
                                {
                                    testGroups[i + 1].Players.Add(firstPlayer);
                                    testGroups[i].Players.Remove(firstPlayer);
                                    isPairFind = false;
                                    break; // перенос игрока в следующую группу, если соперник не найден
                                }
                            }
                            else
                            {
                                isAllPlayersChecked = true;
                            }
                        }

                        index++;
                    }
                    while ((pairsPlayed.Contains(pair)) || (unsuitablePairsForThisLevel.Contains(pair)));

                    if (isPairFind)
                    {
                        List<SwissGroup> groupForTestInside = Cloner.CloneGroups(testGroups);

                        groupForTestInside[i].Players.Remove(firstPlayer);
                        groupForTestInside[i].Players.Remove(secondPlayer);

                        SwissPair pair1 = new SwissPair(firstPlayer.Id, secondPlayer.Id);
                        SwissPair pair2 = new SwissPair(secondPlayer.Id, firstPlayer.Id);

                        if (testGroups.Sum(x => x.Players.Count) >= 20)
                        {
                            testGroups[i].Players.Remove(firstPlayer);
                            testGroups[i].Players.Remove(secondPlayer);
                        }
                        else
                        {
                            if (IsItPossibleToGenerateDraw(groupForTestInside))
                            {
                                testGroups[i].Players.Remove(firstPlayer);
                                testGroups[i].Players.Remove(secondPlayer);
                            }
                            else
                            {
                                unsuitablePairsForThisLevel.Add(pair1);
                                unsuitablePairsForThisLevel.Add(pair2);
                            }
                        }
                    }
                }

                if (testGroups[i].Players.Count == 1)
                {
                    testGroups[i + 1].Players.Add(testGroups[i].Players[0]);
                    testGroups[i].Players.Remove(testGroups[i].Players[0]);
                }
            }

            return true;
        }

        private static void AddGame(SwissTour tour, int gameNumber, SwissPlayer firstPlayer, SwissPlayer secondPlayer)
        {
            if (firstPlayer.ColorPreviestGame == "White" && secondPlayer.ColorPreviestGame == "Black")
            {
                tour.Games.Add(new SwissGame(gameNumber++, secondPlayer.Id, firstPlayer.Id, tour.IdTour));
                firstPlayer.CountGamePlayingWithBlack++;
                secondPlayer.CountGamePlayingWithWhite++;
                firstPlayer.ColorPreviestGame = "Black";
                secondPlayer.ColorPreviestGame = "White";
            }
            else if (firstPlayer.ColorPreviestGame == "Black" && secondPlayer.ColorPreviestGame == "White")
            {
                tour.Games.Add(new SwissGame(gameNumber++, firstPlayer.Id, secondPlayer.Id, tour.IdTour));
                firstPlayer.CountGamePlayingWithWhite++;
                secondPlayer.CountGamePlayingWithBlack++;
                firstPlayer.ColorPreviestGame = "White";
                secondPlayer.ColorPreviestGame = "Black";
            }
            else if (firstPlayer.CountGamePlayingWithWhite > secondPlayer.CountGamePlayingWithWhite)
            {
                tour.Games.Add(new SwissGame(gameNumber++, secondPlayer.Id, firstPlayer.Id, tour.IdTour));
                firstPlayer.CountGamePlayingWithBlack++;
                secondPlayer.CountGamePlayingWithWhite++;
                firstPlayer.ColorPreviestGame = "Black";
                secondPlayer.ColorPreviestGame = "White";
            }
            else if (secondPlayer.CountGamePlayingWithWhite > firstPlayer.CountGamePlayingWithWhite)
            {
                tour.Games.Add(new SwissGame(gameNumber++, firstPlayer.Id, secondPlayer.Id, tour.IdTour));
                firstPlayer.CountGamePlayingWithWhite++;
                secondPlayer.CountGamePlayingWithBlack++;
                firstPlayer.ColorPreviestGame = "White";
                secondPlayer.ColorPreviestGame = "Black";
            }
            else
            {
                int randomNumber = random.Next(0, 2);
                if (randomNumber == 1)
                {
                    tour.Games.Add(new SwissGame(gameNumber++, firstPlayer.Id, secondPlayer.Id, tour.IdTour));
                    firstPlayer.CountGamePlayingWithWhite++;
                    secondPlayer.CountGamePlayingWithBlack++;
                    firstPlayer.ColorPreviestGame = "White";
                    secondPlayer.ColorPreviestGame = "Black";
                }
                else
                {
                    tour.Games.Add(new SwissGame(gameNumber++, secondPlayer.Id, firstPlayer.Id, tour.IdTour));
                    firstPlayer.CountGamePlayingWithBlack++;
                    secondPlayer.CountGamePlayingWithWhite++;
                    firstPlayer.ColorPreviestGame = "Black";
                    secondPlayer.ColorPreviestGame = "White";
                }
            }
        }

        private static List<SwissGroup> SetGroups(List<SwissPlayer> players)
        {
            List<SwissGroup> result = new List<SwissGroup>();
            double maxScore = players.Max(x => x.Score);
            for (double i = 0; i <= maxScore; i += 0.5)
            {
                result.Add(new SwissGroup(players.Where(x => x.Score == i).ToList(), i));
            }

            result.RemoveAll(group => group.Players.Count == 0);
            result = result.OrderByDescending(group => group.Score).ToList();

            return result;
        }

        private static SwissTour DrawFirstRound(List<SwissPlayer> players)
        {
            players = players.OrderByDescending(p => p.Rating).ToList();

            SwissTour tour = new SwissTour(1, 1);

            int halfCount = players.Count / 2;

            int idPlayerSkippingGame = -1;
            if (players.Count % 2 != 0)
            {
                idPlayerSkippingGame = players[players.Count - 1].Id;
                players[players.Count - 1].Score++;
                players[players.Count - 1].DidMissTheTour = true;
            }

            tour.IdPlayerSkippingGame = idPlayerSkippingGame;

            for (int i = 0; i < halfCount; i++)
            {
                SwissPlayer firstPlayer = players[i];
                SwissPlayer secondPlayer = players[i + halfCount];

                if (i % 2 == 0)
                {
                    tour.Games.Add(new SwissGame(i + 1, firstPlayer.Id, secondPlayer.Id, 1));
                    firstPlayer.CountGamePlayingWithWhite++;
                    secondPlayer.CountGamePlayingWithBlack++;
                    firstPlayer.ColorPreviestGame = "White";
                    secondPlayer.ColorPreviestGame = "Black";
                }
                else
                {
                    tour.Games.Add(new SwissGame(i + 1, secondPlayer.Id, firstPlayer.Id, 1));
                    firstPlayer.CountGamePlayingWithBlack++;
                    secondPlayer.CountGamePlayingWithWhite++;
                    firstPlayer.ColorPreviestGame = "Black";
                    secondPlayer.ColorPreviestGame = "White";
                }

                pairsPlayed.Add(new SwissPair(firstPlayer.Id, secondPlayer.Id));
                pairsPlayed.Add(new SwissPair(secondPlayer.Id, firstPlayer.Id));

            }

            return tour;
        }
    }

    
}
