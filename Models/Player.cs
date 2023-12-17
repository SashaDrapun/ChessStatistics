using Microsoft.AspNetCore.Identity;
using System;

namespace ChessStatistics.Models
{
    public class Player : IdentityUser
    {
        public Player(string email, string title)
        {
            this.Email = email;
            this.Title = title;
            this.Rating = 1800;
            this.IsAdmin = false;
        }

        public bool IsAdmin { get; set; }

        public string FIO { get; set; }

        public double Rating { get; set; }

        public string Title { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime DateRegistration { get; set; }

        public DateTime DateLastLogin { get; set; }
    }
}
