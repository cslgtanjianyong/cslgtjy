using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.Watchman
{
    public class CardRecordDataAccess : WatchmanDefaultDataAccess
    {
        public const int DETAIL = 1;

        public CardRecordDataAccess()
        {
            string strSQL;

            strSQL = "SELECT a.Staff, a.Holder, b.IODate, Min(b.IOTime) MinIOTime, MAX(b.IOTime) MaxIOTime "
                   + "  FROM Card a, InOut b "
                   + " WHERE a.CardNo = b.CardNo "
                   + AddCriteria(" AND IODate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" })
                   + " GROUP BY a.Staff, a.Holder, b.IODate "
                   + " ORDER BY b.IODate, a.Staff ";

            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
