using ChessStatistics.Models;
using ChessStatistics.Services;
using ChessStatistics.Services.GameServices;
using ChessStatistics.Services.PlayerServices;
using ChessStatistics.ViewModels;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace ChessStatistics.Mappers
{
    public static class UserMapper
    {
        public static UserModel MapUser(User user)
        {
            UserModel model = new UserModel();

            model.IdPlayer = user.IdPlayer;
            model.IdUser = user.Id;
            model.IsAdmin = user.IsAdmin;
            model.Birthday = user.Birthday;
            model.DateLastLogin = user.DateLastLogin;
            model.DateRegistration = user.DateRegistration;
            model.FIO = user.FIO;
            model.IsUserConnectedToThePlayer = UserSearcher.IsUserConnectedToThePlayer(user);
            
            if (model.IsUserConnectedToThePlayer)
            {
                model.Player = PlayerMapper.MapPlayer(PlayerSearcher.GetPlayerById(user.IdPlayer));
                model.Games = GameMapper.MapGames(GameSearcher.GetGamesByPlayer(user.IdPlayer));
            }

            return model;
        }
    }
}
