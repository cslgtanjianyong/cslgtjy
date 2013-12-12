using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.Flow
{
    public class PALI12 : FlowDefaultDataAccess
    {
        public const int DETAIL = 1;

        public PALI12()
        {
            string strSQL;

            strSQL = "SELECT SerialNumber, ApplyUser, userName ApplyUserName, CreateUserName, WorkType, processSerialNumber,  "
                   + "       ApplyDeptId, hdnWorkType WorkType, CreateUserId, formSerialNumber, LeaveType, "
                   + "       hdnOrganization Organization, ApplyDate, StartDate, EndDate, Memo, IsLeave, "
                   + "       SubstitudeUser, OrganizationId, ApplyDeptName, StartTime, EndTime, "
                   + "       hdnLeaveTypeUnit LeaveTypeUnit, LeaveHours, PALI12.OID, "
                   + "       webServicePara1, webServicePara2, webServicePara3, "
                   + "       webServiceConfirmPara1, webServiceConfirmPara2, StartTimeS, EndTimeS, StartTimeH, EndTimeH, "
                   + "       hdnIsSave IsSave, hdnStartDateTime StartDateTime, hdnEndDateTime EndDateTime, "
                   + "       hdnWorkTypeHour WorkTypeHour, hdnuserids UserIds, hdnDirectManagerLevel DirectManagerLevel, "
                   + "       hdnApplyUserOID ApplyUserOID, hdn_ManagerOrgUnitsByOID ManagerOrgUnitsByOID, Hdn_Textbox_DA DA, HdnOrgUnitOID OrgUnitOID "
                   + "  FROM PALI12 "
                   + "  LEFT JOIN Users ON Users.id = PALI12.ApplyUser "
                   + " WHERE 1 = 1 "
                   + AddCriteria(" AND SerialNumber > @StartSerialNumber", new string[] { "StartSerialNumber" })
                   + AddCriteria(" AND hdnWorkType IN (@WorkType)", new string[] { "WorkType" }, CriteriaTypeEnum.InList)
                   + AddCriteria(" AND LeaveType IN (@LeaveType)", new string[] { "LeaveType" }, CriteriaTypeEnum.InList)
                   + AddCriteria(" AND OrganizationId IN (@OrganizationId)", new string[] { "OrganizationId" }, CriteriaTypeEnum.InList)
                   + AddCriteria(" AND Organization IN (@Organization)", new string[] { "Organization" }, CriteriaTypeEnum.InList)
                   + AddCriteria(" AND hdnLeaveTypeUnit IN (@LeaveTypeUnit)", new string[] { "LeaveTypeUnit" }, CriteriaTypeEnum.InList)
                   + AddCriteria(" AND ApplyDate BETWEEN @StartApplyDate AND @EndApplyDate", new string[] { "StartApplyDate", "EndApplyDate"})
                   + " ORDER BY SerialNumber ";

            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
