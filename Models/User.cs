using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class User : IdentityUser
    {
        public User(string email)
        {
            this.Email = email;
            this.IsAdmin = false;
        }

        [ForeignKey("Player")]
        public int IdPlayer { get; set; }

        public bool IsAdmin { get; set; }

        public string FIO { get; set; }

        public DateTime Birthday { get; set; }

        public DateTime DateRegistration { get; set; }

        public DateTime DateLastLogin { get; set; }

        [NotMapped]
        public Player Player { get; set; }
    }
}
