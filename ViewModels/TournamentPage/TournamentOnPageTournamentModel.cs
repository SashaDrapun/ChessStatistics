﻿using ChessStatistics.Models.Enum;
using System;
using TournamentTypeT = ChessStatistics.Models.Enum.TournamentType;
using RatingTypeT = ChessStatistics.Models.Enum.RatingType;

namespace ChessStatistics.ViewModels.TournamentsPage
{
    public class TournamentOnPageTournamentModel
    {
        public int IdTournament { get; set; }
        public string TournamentName { get; set; }

        public string DateStart { get; set; }
        public string DateFinish { get; set; }

        public TournamentTypeT TournamentType { get; set; }

        public RatingTypeT RatingType { get; set; }

        public OnlineOrOffline OnlineOrOffline { get; set; }

        public string City { get; set; }

        public int MaxRating { get; set; }

        public int MaxAge { get; set; }

        public string TournamentTypeOutput { get; set; }

        public int CountTours { get; set; }

        public string Address { get; set; }

        public string Platform { get; set; }

        public string TournamentLink { get; set; }

        public int MinYear { get; set; }

        public string MaxAgeOutput { get; set; }

        public string MaxRatingOutput { get; set; }

        public bool IsPlatformCalculated { get; set; }

        public int MaxCountPlayers { get; set; }

        public Cost Cost { get; set; }
    }
}
