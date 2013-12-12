using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class RMADutyUnit : MappingTableDataAccess
    {        
        public RMADutyUnit() 
          : base("ID, Name", "sbcRMAFixDutyUnit")
        {
            string strSQL;

            strSQL = "select ID, Name "
                   + "  from sbcRMAFixDutyUnit "
                   + " where 1 = 1 " +
          AddCriteria(" and Name like @DutyUnitName", new string[] { "DutyUnitName" }, CriteriaTypeEnum.Single);
          AddSQLStatement(NAME_FILTER, strSQL);
        }
    }
}
