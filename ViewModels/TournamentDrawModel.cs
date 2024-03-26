using ChessStatistics.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChessStatistics.ViewModels
{
    public class TournamentDrawModel
    {
        public List<TourModel> Tours { get; set; }
    }
}
