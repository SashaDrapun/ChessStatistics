using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChessStatistics.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Player> Players { get; set; }

        public DbSet<LinkUserWithPlayer> LinksUser { get; set; }
        public DbSet<Game> Games { get; set; }

        public DbSet<Tournament> Tournaments { get; set; }

        public DbSet<Tour> Tours { get; set; }

        public DbSet<TournamentParticipants> TournamentParticipants { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
