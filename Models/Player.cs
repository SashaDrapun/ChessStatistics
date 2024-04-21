using ChessStatistics.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdPlayer { get; set; }

        [ForeignKey("User")]

        public string IdUser { get; set; }

        public double RatingClassic { get; set; }

        public double RatingRapid { get; set; }

        public double RatingBlitz { get; set; }

        public Rank Rank { get; set; }

        public string FIO { get; set; }
    }
}
