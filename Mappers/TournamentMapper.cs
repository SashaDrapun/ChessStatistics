using ChessStatistics.Models;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
using ChessStatistics.ViewModels.TournamentsPage;
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
            TournamentModelOnPageTournaments result = new TournamentModelOnPageTournaments();

            result.IdTournament = tournament.IdTournament;
            result.TournamentName = tournament.TournamentName;
            result.TournamentLink = tournament.Link;
            result.OnlineOffline = tournament.OnlineOrOffline;
            result.CountTours = tournament.CountTours;
            result.Address = tournament.Adress;
            result.City = tournament.City;
            result.DateStart = tournament.DateStart.ToString("D");
            result.DateFinish = tournament.DateFinish.ToString("D");
            result.DateStartForAttribute = tournament.DateStart.ToString("yyyy-MM-dd");
            result.DateFinishForAttribute = tournament.DateFinish.ToString("yyyy-MM-dd");
            result.Cost = tournament.Cost;
            result.RatingType = tournament.RatingType;
            result.IsPlatformCalculated = tournament.IsTheTournamentHeldUsingThePlatform;
            result.TournamentType = tournament.TournamentType;
            result.MaxCountPlayers = tournament.MaxCountOfPlayers;
            result.MinYear = tournament.MinimumYearOfBirth;
            result.MaxRating = tournament.MaxRating;
            result.Platform = tournament.Platform;

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
