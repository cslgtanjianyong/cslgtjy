using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class PartType : MappingTableDataAccess
    {
        public const int DETAIL_WITH_REASON = 4;
        public PartType()
            : base("ID, Name", "sbcPartType")
        {
            string strSQL;

            strSQL = "SELECT a.ID PartID, a.Name PartName, b.ID ReasonID, b.Name ReasonName "
                   + "  FROM sbcPartType a, sbcPartBreakReason b "
                   + " WHERE a.ID = b.PartTypeID" +
                   AddCriteria(" AND a.Name LIKE @PartName", new string[] { "PartName" }, CriteriaTypeEnum.Single) +
                   AddCriteria(" AND b.ID LIKE @ReasonID", new string[] { "ReasonID" }, CriteriaTypeEnum.Single) +
                   AddCriteria(" AND b.Name LIKE @ReasonName", new string[] { "ReasonName" }, CriteriaTypeEnum.Single);
            AddSQLStatement(DETAIL_WITH_REASON, strSQL);
        }
    }
}
