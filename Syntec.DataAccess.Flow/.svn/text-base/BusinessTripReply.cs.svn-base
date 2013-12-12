using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.Flow
{
    public class BusinessTripReply: FlowDefaultDataAccess
    {
        public const int DETAIL = 1;

        public BusinessTripReply()
        {
            string strSQL;

            strSQL = "SELECT Textbox_Dept Dept, Hdn_Textbox_UserManeger UserManager, processSerialNumber, Textbox_Phone Phone,"
                   + "       Hdn_Textbox_DA DA, Hdn_Textbox_TripType TripType, Textbox_Date FillDate, "
                   + "       Textbox_Contect Contect, Hdn_Textbox_MultiInput MultiInput, HdnOrgUnitOID OrgUnitOID, "
                   + "       Date_Trip TripDate, Hdn_Textbox_DA_txt DA_txt, Dropdown_TripType, formSerialNumber, "
                   + "       Textbox_Fax Fax, Textbox_UserID UserID, Hdn_Textbox_OtherPerson OtherPerson, "
                   + "       OID, InputMulti_NotifyPeople NotifyPeople, TextArea_Memo Memo, Textbox_CusName CusName, "
                   + "       Textbox_Address Address, Textbox_Series Series, HdnTimeCount TimeCount, hidn_CustomerNo CustomerNo "
                   + "  FROM BusinessTripReply "
                   + " WHERE 1 = 1 "
                   + AddCriteria(" AND Textbox_Date BETWEEN @StartFillDate AND @EndFillDate ", new string[] { "StartFillDate", "EndFillDate" })
                   + AddCriteria(" AND Date_Trip BETWEEN @StartTripDate AND @EndTripDate ", new string[] { "StartTripDate", "EndTripDate" })
                   + AddCriteria(" AND processSerialNumber > @StartProcessSerialNumber", new string[] { "StartProcessSerialNumber" })
                   + " ORDER BY Textbox_Date ";

            AddSQLStatement(DETAIL, strSQL);

        }
    }
}
