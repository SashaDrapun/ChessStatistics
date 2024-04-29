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

            PlayerModel model = new()
            {
                IdPlayer = player.IdPlayer,
                Rank = player.Rank,
                RankOutput = GetRankOutput(player.Rank),
                FIO = player.FIO,
                Rating = new Rating(player.RatingBlitz, player.RatingRapid, player.RatingClassic, tournament.RatingType)
            };
            model.CurrentRating = RatingOperations.GetRating(model.Rating);

            return model;
        }

        public static PlayerOnPagePlayersModel MapPlayerToPlayerOnPlayersPage(Player player)
        {
            PlayerOnPagePlayersModel model = new()
            {
                IdPlayer = player.IdPlayer,
                Rank = player.Rank,
                RankOutput = GetRankOutput(player.Rank),
                FIO = player.FIO,
                RatingBlitz = player.RatingBlitz,
                RatingRapid = player.RatingRapid,
                RatingClassic = player.RatingClassic
            };
            return model;
        }

        public static List<PlayerOnPagePlayersModel> MapListPlayersToListPlayerOnPlayersPage(List<Player> players)
        {
            List<PlayerOnPagePlayersModel> result = [];

            foreach (Player player in players)
            {
                result.Add(MapPlayerToPlayerOnPlayersPage(player));
            }

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
