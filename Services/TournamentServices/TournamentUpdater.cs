using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
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
            tournament.CountTours = tournamentModel.CountTours;
            tournament.TournamentName = tournamentModel.TournamentName;
            tournament.TournamentType = tournamentModel.TournamentType;
            tournament.RatingType = tournamentModel.RatingType;
            Database.db.Tournaments.Update(tournament);
            await Database.db.SaveChangesAsync();
        }
    }
}
