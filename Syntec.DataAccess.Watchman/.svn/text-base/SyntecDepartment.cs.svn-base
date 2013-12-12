using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.Watchman
{
    public class SyntecDepartmentDataAccess : WatchmanDefaultDataAccess
    {
        public const int DETAIL = 1;

        public SyntecDepartmentDataAccess()
        {
            string strSQL;

            strSQL = "SELECT DepartmentName, EmployeeNo, EmployeeName, WorkType "
                   + "  FROM SyntecDepartment "
                   + " WHERE 1 = 1 "
                   + AddCriteria(" AND IsDisplay = @IsDisplay", new string[] { "IsDisplay" })
                   + " ORDER BY DepartmentName, EmployeeNo ";

            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
