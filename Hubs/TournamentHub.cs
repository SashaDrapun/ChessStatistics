using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using ChessStatistics.Models;
using ChessStatistics.ViewModels;
using ChessStatistics.Models.Enum;
using ChessStatistics.BusinessLogic.TournamentResult;

namespace ChessStatistics.Hubs
{
    public class TournamentHub : Hub
    {
        public async Task AddTournamentParticipant(PlayerModel playerModel)
        {
            await Clients.All.SendAsync("AddTournamentParticipant", playerModel);
        }

        public async Task DeleteTournamentParticipant(PlayerModel playerModel)
        {
            await Clients.All.SendAsync("DeleteTournamentParticipant", playerModel);
        }

        public async Task GeneratingTournamentDraw(TournamentDrawModel tournamentDrawModel)
        {
            await Clients.All.SendAsync("GeneratingTournamentDraw", tournamentDrawModel);
        }

        public async Task SetGameResult(GameModel gameModel)
        {
            await Clients.All.SendAsync("SetGameResult", gameModel);
        }

        public async Task UpdateResultTable(RoundRobinTournamentResult roundRobinTournamentResult)
        {
            await Clients.All.SendAsync("UpdateResultTable", roundRobinTournamentResult);
        }
    }
}
