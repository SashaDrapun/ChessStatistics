using ChessStatistics.Models;

namespace ChessStatistics.BusinessLogic
{
    public class Database
    {
        public static ApplicationContext db;

        public static void SetDB(ApplicationContext applicationContext)
        {
            db = applicationContext;
        }
    }
}
