using ChessStatistics.Models;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels;
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
    }
}
