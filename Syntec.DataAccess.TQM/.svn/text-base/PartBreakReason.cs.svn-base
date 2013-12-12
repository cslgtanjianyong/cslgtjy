using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class PartBreakReason : MappingTableDataAccess
    {
        public const int DISTINCT_NAME = 2;
        public const int BY_PART_NAME = 4;

        public const int SELL_OUT = 1;
        public const int IN_HOUSE = 2;


        public PartBreakReason()
            : base("ID, Name, PartTypeID, FixTypeID", "sbcPartBreakReason")
        {
            AddSQLStatement(DISTINCT_NAME, 
                            "SELECT Name, MAX(ID) ID FROM sbcPartBreakReason " +
                            " WHERE 1 = 1 " +
                            AddCriteria(" AND FixTypeID = @FixTypeID ", new string[] {"FixTypeID"}) +
                            AddCriteria(" AND Name LIKE @ReasonName", new string[] { "ReasonName" }, CriteriaTypeEnum.Single) +
                            " GROUP BY Name");

            AddSQLStatement(BY_PART_NAME,
                            "SELECT a.Name, a.ID FROM sbcPartBreakReason a, sbcPartType b " +
                            " WHERE a.PartTypeID = b.ID " +
                            AddCriteria(" AND FixTypeID = @FixTypeID ", new string[] { "FixTypeID" }) +
                            AddCriteria(" AND a.Name LIKE @ReasonName", new string[] { "ReasonName" }) +
                            AddCriteria(" AND b.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                            AddCriteria(" AND b.ID IN (@PartID)", new string[] { "PartID" }, CriteriaTypeEnum.InList));
        }
    }
}
