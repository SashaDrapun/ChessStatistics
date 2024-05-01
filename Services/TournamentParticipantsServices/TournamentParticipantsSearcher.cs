﻿using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChessStatistics.Services.TournamentParticipantsServices
{
    public static class TournamentParticipantsSearcher
    {
        public static List<TournamentParticipants> GetAllTournamentParticipants()
        {
            return Database.db.TournamentParticipants.ToList();
        }

        public static List<int> GetTournamentParticipantsIdPlayerByTournamentId(int tournamentId)
        {
            return Database.db.TournamentParticipants.Where(tp => tp.IdTournament == tournamentId).Select(tp => tp.IdPlayer).ToList();
        }

        public static List<TournamentParticipants> GetTournamentParticipantsByTournamentId(int tournamentId)
        {
            return Database.db.TournamentParticipants.Where(tp => tp.IdTournament == tournamentId).ToList();
        }

        public static List<TournamentParticipants> GetTournamentParticipants(int tournamentId, int idPlayer)
        {
            return Database.db.TournamentParticipants.Where(tp => (tp.IdTournament == tournamentId) && (tp.IdPlayer == idPlayer)).ToList();
        }
    }
}
