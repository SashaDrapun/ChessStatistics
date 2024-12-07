using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.ViewModels;
using ChessStatistics.ViewModels.TournamentsPage;
using System.Numerics;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentServices
{
    public static class TournamentUpdater
    {
        public static async Task UpdateTournamentAsync(EditTournamentModel editTournamentModel)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(editTournamentModel.IdTournament);

            tournament.DateStart = editTournamentModel.DateStart;
            tournament.DateFinish = editTournamentModel.DateFinish;
            tournament.CountTours = editTournamentModel.CountTours;
            tournament.TournamentName = editTournamentModel.TournamentName;
            tournament.TournamentType = editTournamentModel.TournamentType;
            tournament.RatingType = editTournamentModel.RatingType;
            tournament.City = editTournamentModel.City;
            tournament.Adress = editTournamentModel.Address;
            tournament.OnlineOrOffline = editTournamentModel.OnlineOffline;
            tournament.Platform = editTournamentModel.Platform;
            tournament.Link = editTournamentModel.TournamentLink;
            tournament.MinimumYearOfBirth = editTournamentModel.MinYear;
            tournament.MaxRating = editTournamentModel.MaxRating;
            tournament.MaxCountOfPlayers = editTournamentModel.MaxCountPlayers;
            tournament.IsTheTournamentHeldUsingThePlatform = editTournamentModel.IsPlatformCalculated;
            tournament.Cost = editTournamentModel.Cost;

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
