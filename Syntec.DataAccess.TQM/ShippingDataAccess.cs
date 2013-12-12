using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    /*
     * 回傳出貨狀況
     * 只計算 sbcProductMaster 中 TypeSN 以F01, R01及O開頭的項目
     *        sbcHisMaster 中 HisType 只計算Y, L二種
     *        2013/01/24 錦祥與烔堯 預設2010/01/01以前的不顯示
     */

    public class ShippingDataAccess : RMAAbstractDataAcces
    {
        public const int SHIPPING_DETAIL = 1;
        public const int SHIPPING_MONTHLY = 2;
        public const int SHIPPING_QUARTERLY = 4;
        public const int SHIPPING_MONTHLY_MACHINE = 8;

        public ShippingDataAccess()
        {
            string strSQL = "";

            strSQL = "SELECT CONVERT(Varchar(7), LastConsDate, 111) YYYYMM " +
                     "       ,COUNT(DISTINCT ProductSN) ShippingCount " +
                     "  FROM rptShippingDetail " +
                     "  LEFT JOIN rptProductTree ON rptShippingDetail.ProductSN = rptProductTree.SN1 " +
                     " WHERE LastConsDate > '2009/12/31' " +
         AddCriteria("   AND CreateDate BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
         AddCriteria("   AND MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
         AddCriteria("   AND ShippingType in (@ShippingType) ", new string[] { "ShippingType" }, CriteriaTypeEnum.InList) +
         AddCriteria("   AND LastConsDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
         AddCriteria("   AND (TypeSN1 IN (@ShippingTypeSN) OR TypeSN2 IN (@ShippingTypeSN) OR TypeSN3 IN (@ShippingTypeSN) OR TypeSN4 IN (@ShippingTypeSN) OR TypeSN5 IN (@ShippingTypeSN)) ", new string[] { "ShippingTypeSN" }, CriteriaTypeEnum.InList) +
                     " GROUP BY CONVERT(Varchar(7), LastConsDate, 111) " +
                     " ORDER BY YYYYMM ";
            AddSQLStatement(SHIPPING_MONTHLY, strSQL);

            strSQL = "SELECT (CAST(DATEPART(yyyy, LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, LastConsDate) AS Varchar(2)) + 'Q') YYYYQQ " +
                     "       ,COUNT(DISTINCT ProductSN) ShippingCount " +
                     "  FROM rptShippingDetail " +
                     "  LEFT JOIN rptProductTree ON rptShippingDetail.ProductSN = rptProductTree.SN1 " +
                     " WHERE LastConsDate > '2009/12/31' " +
         AddCriteria("   AND CreateDate BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
         AddCriteria("   AND MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
         AddCriteria("   AND ShippingType in (@ShippingType) ", new string[] { "ShippingType" }, CriteriaTypeEnum.InList) +
         AddCriteria("   AND LastConsDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
         AddCriteria("   AND (TypeSN1 IN (@ShippingTypeSN) OR TypeSN2 IN (@ShippingTypeSN) OR TypeSN3 IN (@ShippingTypeSN) OR TypeSN4 IN (@ShippingTypeSN) OR TypeSN5 IN (@ShippingTypeSN)) ", new string[] { "ShippingTypeSN" }, CriteriaTypeEnum.InList) +
                     " GROUP BY (CAST(DATEPART(yyyy, LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, LastConsDate) AS Varchar(2)) + 'Q') " +
                     " ORDER BY YYYYQQ";
            AddSQLStatement(SHIPPING_QUARTERLY, strSQL);

            strSQL = "  FROM rptShippingDetail " +
                     "  LEFT JOIN rptProductTree ON rptShippingDetail.ProductSN = rptProductTree.SN1 " +
                     " WHERE LastConsDate > '2009/12/31' " +
         AddCriteria("   AND CreateDate BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
         AddCriteria("   AND MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
         AddCriteria("   AND ShippingType in (@ShippingType) ", new string[] { "ShippingType" }, CriteriaTypeEnum.InList) +
         AddCriteria("   AND LastConsDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
         AddCriteria("   AND (TypeSN1 IN (@ShippingTypeSN) OR TypeSN2 IN (@ShippingTypeSN) OR TypeSN3 IN (@ShippingTypeSN) OR TypeSN4 IN (@ShippingTypeSN) OR TypeSN5 IN (@ShippingTypeSN)) ", new string[] { "ShippingTypeSN" }, CriteriaTypeEnum.InList);
            AddSQLStatement(SHIPPING_MONTHLY_MACHINE, strSQL);
            //strSQL = "SELECT a.ProductSN " +
            //            "      ,CONVERT(Varchar(7) ,b.LastConsDate, 111) YYYYMM " +
            //            "      ,(CAST(DATEPART(yyyy, b.LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, b.LastConsDate) AS Varchar(2)) + 'Q') YYYYQQ " +
            //            "      ,a.HisTypeNo " +
            //            "      ,b.LastConsDate " +
            //            "      ,a.Cons_User " +
            //            "      ,a.Attr1 " +
            //            "      ,a.Attr2 " +
            //            "      ,a.Attr3 " +
            //            "  FROM sbcHisMaster a, (SELECT a.ProductSN LastProductSN " +
            //            "                              ,MAX(a.Cons_Date) LastConsDate " +
            //            "                              ,MAX(c.MachineType) MachineType " +
            //            "                              ,MAX(b.Cons_Date) CreateDate " +
            //            "                          FROM sbcHisMaster a, sbcProductMaster b, sbcProductType c, sbcProductAttribute d " +
            //            "                         WHERE HisTypeNo IN ('Y', 'L') " +
            //            "                           AND a.ProductSN = b.SN " +
            //            "                           AND b.SN = d.ProductSN " +
            //            "                           AND d.MachineTypeSN = c.TypeSN " +
            //            AddCriteria("               AND b.Cons_Date BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //            AddCriteria("               AND c.MachineType in (@MachineType) ", new string[] {"MachineType"}, CriteriaTypeEnum.InList) +
            //            "                           AND (b.TypeSN LIKE 'F01%' OR b.TypeSN LIKE 'R01%' OR b.TypeSN LIKE 'O%') " +
            //            "                         GROUP BY a.ProductSN) b " +                        
            //            " WHERE a.Cons_Date = b.LastConsDate " +
            //            "   AND a.ProductSN = b.LastProductSN " +
            //            "   AND b.LastConsDate > '2009/12/31' " +
            //            AddCriteria(" and b.LastConsDate between @StartDate and @EndDate ", new string[] {"StartDate", "EndDate"}) +
            //            " ORDER BY YYYYMM";
            //AddSQLStatement(SHIPPING_DETAIL, strSQL);

            //strSQL = "SELECT CONVERT(Varchar(7), b.LastConsDate, 111) YYYYMM " +
            //            "       ,COUNT(*) ShippingCount " +
            //            "  FROM sbcHisMaster a, (SELECT a.ProductSN LastProductSN " +
            //            "                              ,MAX(a.Cons_Date) LastConsDate " +
            //            "                              ,MAX(c.MachineType) MachineType " +
            //            "                              ,MAX(b.Cons_Date) CreatDate " +
            //            "                          FROM sbcHisMaster a, sbcProductMaster b, sbcProductType c, sbcProductAttribute d " +
            //            "                         WHERE HisTypeNo IN ('Y', 'L') " +
            //            "                           AND a.ProductSN = b.SN " +
            //            "                           AND b.SN = d.ProductSN " +
            //            "                           AND d.MachineTypeSN = c.TypeSN " +
            //            AddCriteria("               AND b.Cons_Date between @CreateStartDate and @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //            AddCriteria("               AND c.MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
            //            "                           AND (b.TypeSN LIKE 'F01%' OR b.TypeSN LIKE 'R01%' OR b.TypeSN LIKE 'O%') " +
            //            "                         GROUP BY a.ProductSN) b " +          
            //            " WHERE a.Cons_Date = b.LastConsDate " +
            //            "   AND a.ProductSN = b.LastProductSN " +
            //            "   AND b.LastConsDate > '2009/12/31' " +
            //            AddCriteria(" and b.LastConsDate between @StartDate and @EndDate ", new string[] { "StartDate", "EndDate" }) +
            //            " GROUP BY CONVERT(Varchar(7), b.LastConsDate, 111) " +
            //            " ORDER BY YYYYMM";
            //AddSQLStatement(SHIPPING_MONTHLY, strSQL);

            //strSQL = "SELECT (CAST(DATEPART(yyyy, b.LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, b.LastConsDate) AS Varchar(2)) + 'Q') YYYYQQ " +
            //        "       ,COUNT(*) ShippingCount " +
            //            "  FROM sbcHisMaster a, (SELECT a.ProductSN LastProductSN " +
            //            "                              ,MAX(a.Cons_Date) LastConsDate " +
            //            "                              ,MAX(c.MachineType) MachineType " +
            //            "                              ,MAX(b.Cons_Date) CreateDate " +
            //            "                          FROM sbcHisMaster a, sbcProductMaster b, sbcProductType c, sbcProductAttribute d " +
            //            "                         WHERE HisTypeNo IN ('Y', 'L') " +
            //            "                           AND a.ProductSN = b.SN " +
            //            "                           AND b.SN = d.ProductSN " +
            //            "                           AND d.MachineTypeSN = c.TypeSN " +
            //            AddCriteria("               AND b.Cons_Date between @CreateStartDate and @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //            AddCriteria("               AND c.MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
            //            "                           AND (b.TypeSN LIKE 'F01%' OR b.TypeSN LIKE 'R01%' OR b.TypeSN LIKE 'O%') " +
            //            "                         GROUP BY a.ProductSN) b " + 
            //            " WHERE a.Cons_Date = b.LastConsDate " +
            //            "   AND a.ProductSN = b.LastProductSN " +
            //            "   AND b.LastConsDate > '2009/12/31' " +
            //            AddCriteria(" and b.LastConsDate between @StartDate and @EndDate ", new string[] { "StartDate", "EndDate" }) +
            //            " GROUP BY (CAST(DATEPART(yyyy, b.LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, b.LastConsDate) AS Varchar(2)) + 'Q') " +
            //            " ORDER BY YYYYQQ";
            //AddSQLStatement(SHIPPING_QUARTERLY, strSQL);

/*            
            strSQL = "SELECT CONVERT(Varchar(7), b.LastConsDate, 111) YYYYMM " +
            "       ,b.MachineType " +
            "       ,COUNT(*) ShippingCount " +
            "  FROM sbcHisMaster a, (SELECT a.ProductSN LastProductSN " +
            "                              ,MAX(a.Cons_Date) LastConsDate " +
            "                              ,MAX(b.MachineType) MachineType " +
            "                          FROM sbcHisMaster a, sbcProductMaster b " +
            "                         WHERE HisTypeNo IN ('Y', 'L') " +
            "                           AND a.ProductSN = b.SN " +
            AddCriteria("               AND b.MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
            "                           AND (b.TypeSN LIKE 'F01%' OR b.TypeSN LIKE 'O%') " +
            "                         GROUP BY ProductSN) b " +
            " WHERE a.Cons_Date = b.LastConsDate " +
            "   AND a.ProductSN = b.LastProductSN " +
            "   AND b.LastConsDate > '2009/12/31' " +
            AddCriteria(" and b.LastConsDate between @StartDate and @EndDate ", new string[] { "StartDate", "EndDate" }) +
            " GROUP BY CONVERT(Varchar(7), b.LastConsDate, 111), b.MachineType " +
            " ORDER BY YYYYMM";
            AddSQLStatement(SHIPPING_MONTHLY_MACHINE, strSQL);
 */
            //strSQL = " FROM sbcHisMaster a, (SELECT a.ProductSN LastProductSN " +
            //"                              ,MAX(a.Cons_Date) LastConsDate " +
            //"                              ,MAX(c.MachineType) MachineType " +
            //"                              ,MAX(b.Cons_Date) CreateDate " +
            //"                          FROM sbcHisMaster a, sbcProductMaster b, sbcProductType c, sbcProductAttribute d " +
            //"                         WHERE HisTypeNo IN ('Y', 'L') " +
            //"                           AND a.ProductSN = b.SN " +
            //"                           AND b.SN = d.ProductSN " +
            //"                           AND d.MachineTypeSN = c.TypeSN " +
            //AddCriteria("               AND b.Cons_Date between @CreateStartDate and @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //AddCriteria("               AND c.MachineType in (@MachineType) ", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
            //"                           AND (b.TypeSN LIKE 'F01%' OR b.TypeSN LIKE 'R01%' OR b.TypeSN LIKE 'O%') " +
            //"                         GROUP BY a.ProductSN) b " +
            //" WHERE a.Cons_Date = b.LastConsDate " +
            //"   AND a.ProductSN = b.LastProductSN " +
            //"   AND b.LastConsDate > '2009/12/31' " +
            //AddCriteria(" and b.LastConsDate between @StartDate and @EndDate ", new string[] { "StartDate", "EndDate" });

            //AddSQLStatement(SHIPPING_MONTHLY_MACHINE, strSQL);
        }
        /*
                public override bool ExecuteCommand(object oDataSet)
                {
                    foreach (object oDataName in SQLNameList)
                    {
                        if (NeedExecute(oDataSet, oDataName) == false)
                            continue;

                        if (ExecuteSQL(oDataName, GetSQLStatement(oDataName)) == false)
                            return false;
                    }

                    if ((iDataSet & SHIPPING_DETAIL) == SHIPPING_DETAIL)
                    {
                        strSQL = "SELECT a.ProductSN " +
                                "      ,CONVERT(Varchar(7) " +
                                "      ,b.LastConsDate, 111) YYYYMM " +
                                "      ,a.HisTypeNo " +
                                "      ,b.LastConsDate " +
                                "      ,a.Cons_User " +
                                "      ,a.Attr1 " +
                                "      ,a.Attr2 " +
                                "      ,a.Attr3 " +
                                "  FROM sbcHisMaster a, (SELECT ProductSN LastProductSN " +
                                "                              ,MAX(Cons_Date) LastConsDate " +
                                "                          FROM sbcHisMaster " +
                                "                         WHERE HisTypeNo IN ('Y', 'L') " +
                                "                         GROUP BY ProductSN) b " +
                                " WHERE a.Cons_Date = b.LastConsDate " +
                                "   AND a.ProductSN = b.LastProductSN " +
                                " ORDER BY YYYYMM";

                        if(ExecuteSQL(SHIPPING_DETAIL, strSQL) == false) 
                            return false;
                    }

                    if ((iDataSet & SHIPPING_MONTHLY) == SHIPPING_MONTHLY)
                    {
                        strSQL = "SELECT CONVERT(Varchar(7), b.LastConsDate, 111) YYYYMM " +
                                "       ,COUNT(*) ShippingCount " +
                                "  FROM sbcHisMaster a, (SELECT ProductSN LastProductSN " +
                                "                              ,MAX(Cons_Date) LastConsDate " +
                                "                          FROM sbcHisMaster " +
                                "                         WHERE HisTypeNo IN ('Y', 'L') " +
                                "                         GROUP BY ProductSN) b " +
                                " WHERE a.Cons_Date = b.LastConsDate " +
                                "   AND a.ProductSN = b.LastProductSN " +
                                " GROUP BY CONVERT(Varchar(7), b.LastConsDate, 111) " +
                                " ORDER BY YYYYMM";

                        if (ExecuteSQL(SHIPPING_MONTHLY, strSQL) == false)
                            return false;
                    }

                    if ((iDataSet & SHIPPING_QUARTERLY) == SHIPPING_QUARTERLY)
                    {
                        strSQL = "SELECT (CAST(DATEPART(yyyy, b.LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, b.LastConsDate) AS Varchar(2)) + 'Q') YYYYQQ " +
                                "       ,COUNT(*) ShippingCount " +
                                "  FROM sbcHisMaster a, (SELECT ProductSN LastProductSN " +
                                "                              ,MAX(Cons_Date) LastConsDate " +
                                "                          FROM sbcHisMaster " +
                                "                         WHERE HisTypeNo IN ('Y', 'L') " +
                                "                         GROUP BY ProductSN) b " +
                                " WHERE a.Cons_Date = b.LastConsDate " +
                                "   AND a.ProductSN = b.LastProductSN " +
                                " GROUP BY (CAST(DATEPART(yyyy, b.LastConsDate) AS Varchar(4)) + '.' + CAST(DATEPART(qq, b.LastConsDate) AS Varchar(2)) + 'Q') " +
                                " ORDER BY YYYYQQ";

                        if (ExecuteSQL(SHIPPING_QUARTERLY, strSQL) == false)
                            return false;
                    }

                    return true;
                }
         */
    }
}
