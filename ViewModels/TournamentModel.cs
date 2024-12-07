using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using System;
using System.Collections.Generic;
using TournamentTypeT = ChessStatistics.Models.Enum.TournamentType;
using RatingTypeT = ChessStatistics.Models.Enum.RatingType;
using Microsoft.Identity.Client;
using ChessStatistics.ViewModels.PlayersPage;
using ChessStatistics.ViewModels.TournamentPage;

namespace ChessStatistics.ViewModels
{
    public class TournamentModel
    {
        public int IdTournament { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }

        public DateTime DateFinish { get; set; }

        public TournamentTypeT TournamentType { get; set; }

        public RatingTypeT RatingType { get; set; }

        public int CountTours { get; set; }

        public List<PlayerOnPagePlayersModel> PlayersParticipatingInTournament { get; set; }

        public List<PlayerOnPagePlayersModel> PlayersNotParticipatingInTournament { get; set; }

        public TournamentDrawModel TournamentDrawModel { get; set; }

        public RoundRobinTournamentResult RoundRobinTournamentResult { get; set; }

        public UserModel UserInfo {get; set;}

        public string City { get; set; }

        public string Address { get; set; }

        public OnlineOrOffline OnlineOffline { get; set; }

        public string Platform { get; set; }

        public string TournamentLink { get; set; }

        public int MinYear { get; set; }

        public int MaxRating { get; set; }

        public bool IsPlatformCalculated { get; set; }

        public int MaxCountPlayers { get; set; }

        public bool IsUserSendRequestToParticipateInTournament {get; set; }

        public Cost Cost {get; set;}

        public List<RequestToParticipateInTournamentModel> RequestsToParticipateInTournamentModels {get; set;}

        public string GetTournamentType()
        {
            string result = string.Empty;

            if (TournamentType == TournamentTypeT.Round)
            {
                result = "Круговая система";
            }

            if (TournamentType == TournamentTypeT.Swiss)
            {
                result = "Швейцарская система";
            }

            return result;
        }

        public string GetRatingType()
        {
            string result = string.Empty;

            if (RatingType == RatingTypeT.Blitz)
            {
                result = "Блиц";
            }

            if (RatingType == RatingTypeT.Rapid)
            {
                result = "Рапид";
            }

            if (RatingType == RatingTypeT.Classic)
            {
                result = "Классика";
            }

            return result;
        }
    }
}
