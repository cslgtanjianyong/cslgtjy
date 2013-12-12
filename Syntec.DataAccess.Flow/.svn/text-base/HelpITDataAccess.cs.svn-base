using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.Flow
{
    public class HelpITDataAccess : FlowDefaultDataAccess
    {
        public const int DETAIL = 1;

        public HelpITDataAccess()
        {
            string strSQL;

            strSQL = "SELECT SerialNumber, date_Hope HopeDate, txta_Complete Complete, rbtn_satisfied Satisfied, "
                   + "       processSerialNumber, txtb_Date ApplyDate, txtb_Department Department, txta_Opinion Opinion, " 
                   + "       txta_Receive Receive, drpd_Demand Demand, txtb_WorkDate ManDay, txtb_JIRAID JIRAID, "
                   + "       hdn_CreaterID CreaterID, txtb_Creater Creater, drpd_Demand1 Demand1, formSerialNumber, "
                   + "       dinl_Application Application, OID, dinl_Process Process, txta_Demand DemandProblem, "
                   + "       txtb_Demand DemandDesc, txtb_child Child, drpd_product Product, Dropdown12, drpd_Child DrpdChild "
                   + "  FROM HelpIT "
                   + " WHERE 1 = 1 "
                   + AddCriteria(" AND date_Hope BETWEEN @StartHopeDate AND @EndHopeDate", new string[] { "StartHopeDate", "EndHopeDate"})
                   + AddCriteria(" AND ApplyDate BETWEEN @StartApplyDate AND @EndApplyDate", new string[] { "StartApplyDate", "EndApplyDate" })
                   + AddCriteria(" AND drpd_Demand IN (@Demand)", new string[] { "Demand" }, CriteriaTypeEnum.InList);


            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
