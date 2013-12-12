using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class MachineType : MappingTableDataAccess
    {        
        public MachineType()
            : base("ID, Name", "sbcMachineType")
        {
            string strSQL;

            strSQL = "SELECT ID, Name "
                   + "  FROM sbcMachineType "
                   + " WHERE 1 = 1 " 
            + AddCriteria(" AND Name LIKE @MachineTypeName", new string[] { "MachineTypeName" }, CriteriaTypeEnum.Single)
            + " ORDER BY Name ";
            AddSQLStatement(NAME_FILTER, strSQL);
        }
    }
}
