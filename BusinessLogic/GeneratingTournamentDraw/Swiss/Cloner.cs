using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public static class Cloner
    {
        public static List<SwissGroup> CloneGroups(List<SwissGroup> groups)
        {
            List<SwissGroup> result = new List<SwissGroup>();

            foreach (var group in groups)
            {
                result.Add(new SwissGroup(group));
            }

            return result;
        }
    }
}
