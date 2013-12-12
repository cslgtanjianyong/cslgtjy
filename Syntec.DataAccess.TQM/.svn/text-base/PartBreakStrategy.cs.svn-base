using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class PartBreakStrategy : MappingTableDataAccess
    {
        public const int DISTINCT_NAME = 2;
        public const int BY_PART_NAME = 4;

        public const int SELL_OUT = 1;
        public const int IN_HOUSE = 2;


        public PartBreakStrategy()
            : base("ID, Name, PartTypeID, FixTypeID", "sbcPartBreakStrategy")
        {
            AddSQLStatement(DISTINCT_NAME,
                            "SELECT Name, MAX(ID) ID FROM sbcPartBreakStrategy " +
                            " WHERE 1 = 1 " +
                            AddCriteria(" AND FixTypeID = @FixTypeID ", new string[] { "FixTypeID" }) +
                            AddCriteria(" AND Name LIKE @StrategyName", new string[] { "StrategyName" }, CriteriaTypeEnum.Single) +
                            " GROUP BY Name");

            AddSQLStatement(BY_PART_NAME,
                            "SELECT a.Name, a.ID FROM sbcPartBreakStrategy a, sbcPartType b " +
                            " WHERE a.PartTypeID = b.ID " +
                            AddCriteria(" AND FixTypeID = @FixTypeID ", new string[] { "FixTypeID" }) +
                            AddCriteria(" AND a.Name LIKE @StrategyName", new string[] { "StrategyName" }) +
                            AddCriteria(" AND b.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                            AddCriteria(" AND b.ID IN (@PartID)", new string[] { "PartID" }, CriteriaTypeEnum.InList));
        }
    }
}
