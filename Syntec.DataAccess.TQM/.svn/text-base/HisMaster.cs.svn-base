using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class HisMaster : RMAAbstractDataAcces
    {
        public const int DETAIL = 1;

        public HisMaster()
        {
            string strSQL;

            strSQL = "SELECT ProductSN, HisTypeNO, Cons_Date, Cons_User, Attr1, Attr2, Attr3 "
                   + "  FROM sbcHisMaster "
                   + " WHERE 1 = 1 "
         + AddCriteria(" AND Cons_Date >= @StartConsDate", new string[] { "StartConsDate" })
         + AddCriteria(" AND ProductSN = @ProductSN", new string[] { "ProductSN" })
         + AddCriteria(" AND HisTypeNO IN (@HisTypeNOList)", new string[] { "HisTypeNOList" }, CriteriaTypeEnum.InList);
            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
