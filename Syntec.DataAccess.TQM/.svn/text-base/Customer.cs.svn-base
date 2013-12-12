using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class Customer : MappingTableDataAccess
    {
        public const int CALL_CUSTOMER_ID = 4;
        public const int RPT_CUSTOMER = 8;
        public Customer()
            : base("Code, SName Name", "TEMP_CUSTOMER_NAME")
        {
            AddSQLStatement(CALL_CUSTOMER_ID, "SELECT Code, SName Name FROM TEMP_CUSTOMER_NAME WHERE Code IN (SELECT DISTINCT CustomerId FROM sbcRMACallMaster)");

            string strSQL;

            strSQL = "SELECT Code, SName Name "
                   + "  FROM TEMP_CUSTOMER_NAME "
                   + " WHERE Code IN (SELECT DISTINCT CustomerId FROM sbcRMACallMaster) " +
            AddCriteria(" AND SName LIKE @CustomerName", new string[] { "CustomerName" }, CriteriaTypeEnum.Single);
            AddSQLStatement(NAME_FILTER, strSQL);

            strSQL = "SELECT RPTCustomerName Name "
                   + "  FROM rptCustomer "
                   + " WHERE 1 = 1 "
            + AddCriteria(" AND RPTCustomerName LIKE @CustomerName", new string[] { "CustomerName" }, CriteriaTypeEnum.Single) 
                   +" GROUP BY RPTCustomerName "
                   + " HAVING SUM(RefCount) >= 3 ";
            AddSQLStatement(RPT_CUSTOMER, strSQL);
        }
    }
}
