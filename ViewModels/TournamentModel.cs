using ChessStatistics.BusinessLogic.TournamentResult;
using ChessStatistics.Models;
using ChessStatistics.Models.Enum;
using System;
using System.Collections.Generic;
using TournamentTypeT = ChessStatistics.Models.Enum.TournamentType;
using RatingTypeT = ChessStatistics.Models.Enum.RatingType;


namespace ChessStatistics.ViewModels
{
    public class TournamentModel
    {
        public int IdTournament { get; set; }

        public string TournamentName { get; set; }

        public DateTime DateStart { get; set; }

        public TournamentTypeT TournamentType { get; set; }

        public RatingTypeT RatingType { get; set; }

        public int CountTours { get; set; }

        public List<Player> PlayersParticipatingInTournament { get; set; }

        public List<Player> PlayersNotParticipatingInTournament { get; set; }

        public TournamentDrawModel TournamentDrawModel { get; set; }

        public RoundRobinTournamentResult RoundRobinTournamentResult { get; set; }

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
