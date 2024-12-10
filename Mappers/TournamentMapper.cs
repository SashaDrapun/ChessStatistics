using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using ChessStatistics.ViewModels.TournamentsPage;
using System;
using System.Collections.Generic;

namespace ChessStatistics.Mappers
{
    public static class TournamentMapper
    {
        public static List<TournamentModel> MapTournamentsWithPartialInformation(List<Tournament> tournaments)
        {
            List<TournamentModel> result = new List<TournamentModel>();

            foreach (var tournament in tournaments)
            {
                result.Add(TournamentSearcher.GetPartialTournamentModel(tournament.IdTournament));
            }

            return result;
        }

        public static TournamentModelOnPageTournaments MapTournamentToTournamentOnPageTournamentsModel(Tournament tournament)
        {
            var result = new TournamentModelOnPageTournaments
            {
                IdTournament = tournament.IdTournament,
                TournamentName = tournament.TournamentName,
                TournamentLink = tournament.Link,
                CountTours = tournament.CountTours,
                Address = tournament.Adress,
                City = tournament.City,
                DateStart = tournament.DateStart.ToString("D"),
                DateFinish = tournament.DateFinish.ToString("D"),
                DateStartForAttribute = tournament.DateStart.ToString("yyyy-MM-dd"),
                DateFinishForAttribute = tournament.DateFinish.ToString("yyyy-MM-dd"),
                Cost = tournament.Cost,
                RatingType = tournament.RatingType,
                IsPlatformCalculated = tournament.IsTheTournamentHeldUsingThePlatform,
                TournamentType = tournament.TournamentType,
                MaxCountPlayers = tournament.MaxCountOfPlayers,
                MinYear = tournament.MinimumYearOfBirth,
                Platform = tournament.Platform,
                TournamentFilter = new TournamentFilter()
            };

           
            DateTime currentDate = DateTime.Now;

            if (currentDate < tournament.DateStart)
            {
                result.TournamentFilter.DateStatus = DateStatus.Planned;
            }
            else if (currentDate >= tournament.DateStart && currentDate <= tournament.DateFinish)
            {
                result.TournamentFilter.DateStatus = DateStatus.Current;
            }
            else
            {
                result.TournamentFilter.DateStatus = DateStatus.Past;
            }

            result.TournamentFilter.OnlineOrOffline = tournament.OnlineOrOffline;
            result.TournamentFilter.City = tournament.City;
            result.TournamentFilter.MaxRating = tournament.MaxRating;
            result.TournamentFilter.MaxAge = currentDate.Year - tournament.MinimumYearOfBirth;
            result.MaxAgeOutput = tournament.MinimumYearOfBirth == 1900 ? "Без ограничений по возврасту"
                                                                    : $"До {result.TournamentFilter.MaxAge} лет включительно";
            result.MaxRatingOutput = tournament.MaxRating == 3300 ? "Без ограничений по рейтингу"
                                                                    : $"До {result.TournamentFilter.MaxRating} включительно";
            if (result.TournamentType == TournamentType.Round)
            {
                result.TournamentTypeOutput = "Круговая система";
            }
            else if(result.TournamentType == TournamentType.Swiss)
            {
                result.TournamentTypeOutput = "Швейцарская система";
            }
            
            return result;
        }

        public static List<TournamentModelOnPageTournaments> MapTournamentsToTournamentsOnPageTournamentsModel(List<Tournament> tournaments)
        {
            List<TournamentModelOnPageTournaments> result = new List<TournamentModelOnPageTournaments>();

            foreach (var tournament in tournaments)
            {
                result.Add(MapTournamentToTournamentOnPageTournamentsModel(tournament));
            }

            return result;
        }
    }
}
