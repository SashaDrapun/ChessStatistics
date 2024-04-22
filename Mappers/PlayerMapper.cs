using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.Services.ToursServices;
using ChessStatistics.ViewModels;
using System.Collections.Generic;
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
            model.RankOutput = GetRankOutput(player.Rank);
            model.FIO = player.FIO;
            model.Rating = new Rating(player.RatingBlitz, player.RatingRapid, player.RatingClassic, tournament.RatingType);
            model.CurrentRating = RatingOperations.GetRating(model.Rating);

            return model;
        }

        public static PlayerOnPagePlayersModel MapPlayerToPlayerOnPlayersPage(Player player)
        {
            PlayerOnPagePlayersModel model = new PlayerOnPagePlayersModel();

            model.IdPlayer = player.IdPlayer;
            model.Rank = player.Rank;
            model.RankOutput = GetRankOutput(player.Rank);
            model.FIO = player.FIO;
            model.RatingBlitz = player.RatingBlitz;
            model.RatingRapid = player.RatingRapid;
            model.RatingClassic = player.RatingClassic;
            return model;
        }

        public static List<PlayerOnPagePlayersModel> MapListPlayersToListPlayerOnPlayersPage(List<Player> players)
        {
            List<PlayerOnPagePlayersModel> result = new List<PlayerOnPagePlayersModel>();

            foreach (Player player in players)
            {
                result.Add(MapPlayerToPlayerOnPlayersPage(player));
            }
            PlayerOnPagePlayersModel model = new PlayerOnPagePlayersModel();

            return result;
        }

        private static string GetRankOutput(Rank rank)
        {
            if (rank == Rank.Fourth)
            {
                return "4 разряд";
            }

            if (rank == Rank.Third)
            {
                return "3 разряд";
            }

            if (rank == Rank.Second)
            {
                return "2 разряд";
            }

            if (rank == Rank.First)
            {
                return "1 разряд";
            }

            if (rank == Rank.Kms)
            {
                return "Кандидат в мастера спорта";
            }

            if (rank == Rank.FM)
            {
                return "Фиде мастер";
            }

            if (rank == Rank.IM)
            {
                return "Международный мастер";
            }

            if (rank == Rank.GM)
            {
                return "Гроссмейстер";
            }

            return "Без разряда";

        }

         
    }
}
