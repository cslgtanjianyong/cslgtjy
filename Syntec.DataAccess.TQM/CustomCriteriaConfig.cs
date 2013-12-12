using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class CustomCriteriaConfig : RMAAbstractDataAcces
    {
        public const int CONFIG_NAME = 1;
        public const int DETAIL = 2;

        public CustomCriteriaConfig()
        {
            string strSQL;

            strSQL = "SELECT DISTINCT ConfigName  "
                   + "  FROM rptCustomCriteriaConfig "
                   + " WHERE 1 = 1 "
         + AddCriteria(" AND UserId = @UserId", new string[] { "UserId" });
            
            AddSQLStatement(CONFIG_NAME, strSQL);


            strSQL = "SELECT DISTINCT ConfigName, Seq, CriteriaName, CriteriaValue  "
                   + "  FROM rptCustomCriteriaConfig "
                   + " WHERE 1 = 1 "
         + AddCriteria(" AND UserId = @UserId", new string[] { "UserId" })
         + AddCriteria(" AND ConfigName = @ConfigName", new string[] { "ConfigName" })
         + " ORDER BY Seq ";
            
            AddSQLStatement(DETAIL, strSQL);

        }
    }
}
