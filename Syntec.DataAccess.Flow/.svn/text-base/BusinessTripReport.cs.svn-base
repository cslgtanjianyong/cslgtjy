using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.Flow
{
    public class BusinessTripReport : FlowDefaultDataAccess
    {
        public const int DETAIL = 1;

        public BusinessTripReport()
        {
            string strSQL;

            strSQL = "SELECT Textbox_CRMNum CRMNum, Textbox_TripMission TripMission, Textbox TripCus TripCus, Hdn_Textbox_CRM CRM,"
                   + "       Textbox_Dept Dept, processSerialNumber, Textbox_Date FillDate, "
                   + "       Textbox_TripLocation TripLocation, Date_Trip TripDate, formSerialNumber, "
                   + "       Textbox_UserID UserID, TextArea_Mission Mission, OID, Textbox_Series Series "
                   + "  FROM BusinessTripReport "
                   + " WHERE 1 = 1 "
                   + AddCriteria(" AND Textbox_Date BETWEEN @StartFillDate AND @EndFillDate ", new string[] { "StartFillDate", "EndFillDate" })
                   + AddCriteria(" AND Date_Trip BETWEEN @StartTripDate AND @EndTripDate ", new string[] { "StartTripDate", "EndTripDate" });

            AddSQLStatement(DETAIL, strSQL);

        }
    }
}
