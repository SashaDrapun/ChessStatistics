using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ChessStatistics.Models
{
    public class TournamentRequests
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Tournament")]
        public int IdTournament { get; set; }

        [ForeignKey("User")]
        public string IdUser { get; set; }

        public byte[] Photo { get; set; }
    }
}