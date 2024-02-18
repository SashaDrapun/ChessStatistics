using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ChessStatistics.Models
{
    public class LinkUserWithPlayer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string IdUser { get; set; }

        [ForeignKey("Player")]
        public int IdPlayer { get; set; }

        [NotMapped]
        public Player Player { get; set; }

        [NotMapped]
        public User User { get; set; }
    }
}
