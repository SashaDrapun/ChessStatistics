using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using ChessStatistics.Services;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.Services.TournamentServices;
using ChessStatistics.ViewModels.TournamentPage;

namespace ChessStatistics.Mappers
{
    public static class RequestToParticipateInTournamentMapper
    {
        public static RequestToParticipateInTournamentModel MapRequestToParticipateInTournament(TournamentRequests tournamentRequests)
        {
            RequestToParticipateInTournamentModel result = new RequestToParticipateInTournamentModel();

            result.IdTournament = tournamentRequests.IdTournament;
            result.IdUser = tournamentRequests.IdUser;
            
            User user = UserSearcher.GetUserById(tournamentRequests.IdUser);
            Player player = PlayerSearcher.GetPlayerByIdUser(user.Id);
            RatingType ratingType = TournamentSearcher.GetTournamentRatingTypeById(tournamentRequests.IdTournament);

            result.PlayerFIO = player.FIO;
            result.PlayerRank = player.Rank.ToString();
            result.PlayerRating = RatingCalculation.GetCurrentRating(player.RatingBlitz, player.RatingRapid, player.RatingClassic, ratingType);

            return result;
        }

        public static List<RequestToParticipateInTournamentModel> MapRequestsToParticipateInTournament(List<TournamentRequests> tournamentRequests)
        {
            var result = new List<RequestToParticipateInTournamentModel>();

            foreach (var tournamentRequest in tournamentRequests)
            {
                result.Add(MapRequestToParticipateInTournament(tournamentRequest));
            }

            return result;
        }
    }
}