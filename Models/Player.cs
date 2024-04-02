using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.Models
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("User")]

        public string IdUser { get; set; }

        public double RatingClassic { get; set; }

        public double RatingRapid { get; set; }

        public double RatingBlitz { get; set; }

        public string Title { get; set; }

        public string FIO { get; set; }
    }
}
