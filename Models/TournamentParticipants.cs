using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class TournamentParticipants
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Tournament")]
        public int IdTournament { get; set; }

        [ForeignKey("Player")]
        public int IdPlayer { get; set;  }
    }
}
