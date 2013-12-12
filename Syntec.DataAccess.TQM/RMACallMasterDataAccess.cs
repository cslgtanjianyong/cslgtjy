using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class RMACallMasterDataAccess : RMAAbstractDataAcces
    {
        public static int GROUP_BY_PART_AND_MACHINE_TYPE = 1;

        public RMACallMasterDataAccess()
        {

            string strSQL = @"WITH ProductCount AS
                                (
	                                SELECT Convert(Varchar(7), Cons_Date, 111) ConsMonth, ISNULL(c.Name, '無') Name, ISNULL(b.MachineType, '無') MachineType, COUNT(DISTINCT a.SN) ProductCount
	                                  FROM sbcProductMaster a
	                                  LEFT JOIN rptProductTree d ON (a.SN = d.SN1)	  
	                                  LEFT JOIN sbcProductType b ON (d.TypeSN1 = b.TypeSN)
	                                  LEFT JOIN sbcPartType c ON (b.PartTypeID = c.ID)	  
	                                 GROUP BY Convert(Varchar(7), Cons_Date, 111), Name, b.MachineType
                                ),
                                CallCount AS
                                (
	                                SELECT CONVERT(Varchar(7), CallDate, 111) CallMonth, ISNULL(c.Name, '無') Name, ISNULL(b.MachineType, '無') MachineType, COUNT(DISTINCT a.CallID) CallCount
	                                  FROM sbcRMACallMaster a
	                                  LEFT JOIN rptProductTree d ON (a.ProductSN = d.SN1)	  
	                                  LEFT JOIN sbcProductType b ON (d.TypeSN1 = b.TypeSN)
	                                  LEFT JOIN sbcPartType c ON (b.PartTypeID = c.ID)	  
	                                 WHERE TypeID LIKE '%1002'
	                                 GROUP BY Convert(Varchar(7), CallDate, 111), Name, b.MachineType
                                ),
                                ProductList AS (
		                                SELECT ConsMonth ProductMonth, Name, MachineType
		                                  FROM ProductCount
		                                UNION ALL
		                                SELECT CallMonth ProductMonth, Name, MachineType
		                                  FROM CallCount
                                ),
                                DistinctProductList AS (
		                                SELECT DISTINCT ProductMonth, Name, MachineType
		                                  FROM ProductList
                                ),
                                ProductSummary AS (
                                SELECT a.ProductMonth, a.Name, a.MachineType
	                                  , CASE a.Name WHEN '控制器成品類' THEN a.MachineType ELSE a.Name END GroupName
	                                  , ISNULL(c.CallCount, 0) CallCount, ISNULL(b.ProductCount, 0) ProductCount
                                  FROM DistinctProductList a
                                  LEFT JOIN ProductCount b ON (a.ProductMonth = b.ConsMonth AND a.Name = b.Name AND a.MachineType = b.MachineType)
                                  LEFT JOIN CallCount c ON  (a.ProductMonth = c.CallMonth AND a.Name = c.Name AND a.MachineType = c.MachineType)
                                ),
                                ReportSummary AS (
                                SELECT ProductMonth, GroupName, SUM(CallCount) CallCountSum, SUM(ProductCount) ProductCountSum
                                  FROM ProductSummary
                                 GROUP BY ProductMonth, GroupName
                                 ) "
                + " SELECT ProductMonth, GroupName, CallCountSum, ProductCountSum, "
                + "        CASE ProductCountSum WHEN 0 THEN 0 "
                + "                          ELSE CONVERT(decimal(10, 2), ISNULL(CallCountSum, 0) * 100.0 / ProductCountSum) END Ratio "
                + "   FROM ReportSummary "
                + "  WHERE 1 = 1 "
                + AddCriteria(" AND ProductMonth BETWEEN @StartMonth AND @EndMonth ", new string[] { "StartMonth", "EndMonth" })
                + AddCriteria(" AND CallCountSum > @MinCallCount ", new string[] { "MinCallCount"})
                + "  ORDER BY ProductMonth, GroupName ";

            AddSQLStatement(GROUP_BY_PART_AND_MACHINE_TYPE, strSQL);
        }
    }
}
