using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.ViewModels;
using System.Numerics;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentServices
{
    public static class TournamentUpdater
    {
        public static async Task UpdateTournamentAsync(TournamentModel tournamentModel)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(tournamentModel.IdTournament);

            tournament.DateStart = tournamentModel.DateStart;
            tournament.DateFinish = tournamentModel.DateFinish;
            tournament.CountTours = tournamentModel.CountTours;
            tournament.TournamentName = tournamentModel.TournamentName;
            tournament.TournamentType = tournamentModel.TournamentType;
            tournament.RatingType = tournamentModel.RatingType;
            tournament.City = tournamentModel.City;
            tournament.Adress = tournamentModel.Address;
            tournament.OnlineOrOffline = tournamentModel.OnlineOffline;
            tournament.Platform = tournamentModel.Platform;
            tournament.Link = tournamentModel.TournamentLink;
            tournament.MinimumYearOfBirth = tournamentModel.MinYear;
            tournament.MaxRating = tournamentModel.MaxRating;
            tournament.MaxCountOfPlayers = tournamentModel.MaxCountPlayers;
            tournament.IsTheTournamentHeldUsingThePlatform = tournamentModel.IsPlatformCalculated;
            tournament.Cost = tournamentModel.Cost;

            Database.db.Tournaments.Update(tournament);
            await Database.db.SaveChangesAsync();
        }

        public static async Task SetCountToursAsync(int tournamentId)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(tournamentId);

            int countPlayersParticipatingInTournament = PlayerSearcher.GetPlayersParticipatingOrNotParticipatingInTournament(tournamentId, true).Count;

            tournament.CountTours = (countPlayersParticipatingInTournament % 2 == 0) ? (countPlayersParticipatingInTournament - 1) : countPlayersParticipatingInTournament;
            Database.db.Tournaments.Update(tournament);
            await Database.db.SaveChangesAsync();
        }
    }
}
