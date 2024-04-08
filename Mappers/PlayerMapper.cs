using ChessStatistics.Models;
using ChessStatistics.Services;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System.Reflection.Metadata.Ecma335;

namespace ChessStatistics.Mappers
{
    public static class PlayerMapper
    {
        public static PlayerModel MapPlayer(Player player, int idTournament)
        {
            Tournament tournament = TournamentSearcher.GetTournamentById(idTournament);

            PlayerModel model = new PlayerModel();

            model.IdPlayer = player.IdPlayer;
            model.Rank = player.Rank;
            model.FIO = player.FIO;
            model.Rating = new Rating(player.RatingBlitz, player.RatingRapid, player.RatingClassic, tournament.RatingType);
            model.CurrentRating = RatingOperations.GetRating(model.Rating);

            return model;
        }

         
    }
}
