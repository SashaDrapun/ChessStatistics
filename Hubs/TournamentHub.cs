using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using ChessStatistics.Models;

namespace ChessStatistics.Hubs
{
    public class TournamentHub : Hub
    {
        public async Task Send(int playerId, string playerFIO, double playerRating, string playerTitle)
        {
            await Clients.All.SendAsync("Send", playerId, playerFIO, playerRating, playerTitle.ToString());
        }

    }
}
