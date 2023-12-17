using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChessStatistics.Models
{
    public class ApplicationContext : IdentityDbContext<Player>
    {
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
