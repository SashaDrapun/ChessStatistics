using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChessStatistics.BusinessLogic;
using ChessStatistics.Models;
using ChessStatistics.ViewModels.TournamentPage.RequestToParticipateInTournamentModels;

namespace ChessStatistics.Services.RequestsToParticipateInTournamentServices
{
    public static class RequestToParticipateInTournamentAdder
    {
        public static async Task<TournamentRequests> AddTournamentRequestAsync(AddRequestToParticipateInTournamentModel addRequestToParticipateInTournamentModel)
        {
            TournamentRequests tournamentRequests = new()
            {
                IdTournament = addRequestToParticipateInTournamentModel.IdTournament,
                IdUser = addRequestToParticipateInTournamentModel.IdUser
            };

            Database.db.TournamentRequests.Add(tournamentRequests);
            await Database.db.SaveChangesAsync();
            return tournamentRequests;
        }
    }
}