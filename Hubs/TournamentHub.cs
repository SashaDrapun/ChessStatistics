using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using ChessStatistics.Models;
using ChessStatistics.ViewModels;
using ChessStatistics.Models.Enum;

namespace ChessStatistics.Hubs
{
    public class TournamentHub : Hub
    {
        public async Task AddTournamentParticipant(int playerId, string playerFIO, double playerRating, string playerTitle)
        {
            await Clients.All.SendAsync("AddTournamentParticipant", playerId, playerFIO, playerRating, playerTitle.ToString());
        }

        public async Task GeneratingTournamentDraw(TournamentDrawModel tournamentDrawModel)
        {
            await Clients.All.SendAsync("GeneratingTournamentDraw", tournamentDrawModel);
        }

        public async Task SetGameResult(GameModel gameModel)
        {
            await Clients.All.SendAsync("SetGameResult", gameModel);
        }
    }
}
