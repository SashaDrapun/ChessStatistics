using System;

namespace ChessStatistics.ViewModels.RegisterPage
{
    public class RegisterModel
    {
        public string FIO { get; set; }

        public string Title { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public DateTime Birthday { get; set; }
    }
}
