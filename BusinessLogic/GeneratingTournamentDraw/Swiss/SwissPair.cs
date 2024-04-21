using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ChessStatistics.BusinessLogic.GeneratingTournamentDraw.Swiss
{
    public class SwissPair : IComparer<SwissPair>
    {
        public SwissPair(int idFirstPlayer, int idSecondPlayer)
        {
            IdFirstPlayer = idFirstPlayer;
            IdSecondPlayer = idSecondPlayer;
        }

        int IdFirstPlayer {  get; set; }

        int IdSecondPlayer { get; set; }

        public int Compare(SwissPair x, SwissPair y)
        {
            if (x is null)
            {
                throw new NullReferenceException();
            }

            if (y is null)
            {
                throw new NullReferenceException();
            }


            if (x.GetHashCode() > y.GetHashCode())
            {
                return 1;
            }

            if (x.GetHashCode() < y.GetHashCode())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is SwissPair pair &&
                   IdFirstPlayer == pair.IdFirstPlayer &&
                   IdSecondPlayer == pair.IdSecondPlayer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdFirstPlayer, IdSecondPlayer) + HashCode.Combine(IdSecondPlayer, IdFirstPlayer);
        }

        public static bool operator ==(SwissPair pair, SwissPair pair2)
        {
            if (pair.GetHashCode() == pair2.GetHashCode())
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public static bool operator !=(SwissPair pair, SwissPair pair2)
        {
            return !(pair == pair2);
        }
    }
}
