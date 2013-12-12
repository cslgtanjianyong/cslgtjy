using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Syntec.Base;

namespace Syntec.DataAccess.TQM
{
    /*
     * sbcPartBreakReason的Name為 檢測為良品, 檢測正常, 軟體問題, 軟體異常, 借用歸還, 要求翻新 的單號預設會過濾掉     
     * sbcRMAFixMaster 的 TypeID Like '%2001'
     * sbcRMAFixDutyUnit 的 Name 扣除 研發部及客戶
     * UseTime >= 0 and UseTime <> 9999 and UseTime < 48 小於48的條件是因目前報表只產生至47個月, 將來可改為動態給定
     * 2013/01/24 錦祥與烔堯 預設2010/01/01以前的不顯示
     */
    public class RMADetailDataAccess : RMAAbstractDataAcces
    {
        private const string FILTER_REASON = "'檢測為良品', '檢測正常', '要求翻新', '借用歸還', '軟體異常', '軟體問題'";

        public const int RMA_FIX_DETAIL = 1;
        public const int RMA_FIX_MONTHLY = 2;
        public const int RMA_FIX_MONTHLY_BY_PART_TYPE = 4;
        public const int RMA_FIX_MONTHLY_BY_PART_REASON = 8;
        public const int RMA_FIX_GENERAL_GROUP_BY = 16;
        public const int RMA_FIX_QUOTERLY_BY_PART_TYPE = 32;

        public override bool BeforeExecute()
        {
            //維修單號需將日期轉成序號
            //if(GetParam("StartDate") != null)
            //    if(!NumericHelper.IsNumeric(GetParam("StartDate")))                
            //        AddParam("StartDate", DataConvert.SeqFromDate(GetParam("StartDate").ToString(), DataConvert.START));
            //if(GetParam("EndDate") != null)
            //    if (!NumericHelper.IsNumeric(GetParam("EndDate")))                
            //        AddParam("EndDate", DataConvert.SeqFromDate(GetParam("EndDate").ToString(), DataConvert.END));

            //出廠日期需將日期轉成月份
            //if (GetParam("ShippingStartDate") != null)
            //    AddParam("ShippingStartDate", DateHelper.Parse(GetParam("ShippingStartDate").ToString()).ToString("yyyy/MM"));
            //if (GetParam("ShippingEndDate") != null)
            //    AddParam("ShippingEndDate", DateHelper.Parse(GetParam("ShippingEndDate").ToString()).ToString("yyyy/MM"));

            return base.BeforeExecute();
        }

        public RMADetailDataAccess()
        {
            string strSQL = "";

                   strSQL = "SELECT '' No, " +
                            "       叫修單號, " +
                            "       維修單號, " +
                            "       出廠日期, " +
                            //"       最後刷出廠日期, " +
                            //"       回推出廠日期, " +
                            "       製造日期, " +
                            "       叫修日期, " +
                            "       維修日期, " +
                            "       維修單類別, " +
                            //"       產品分類, " +
                            "       客戶簡稱, " +
                            "       TQM客戶簡稱, " +
                            "       產品品序 產品料號, " +
                            "       產品品名 母件產品品名, " +
                            "       產品規格 母件產品規格, " +
                            "       產品序號, " +
                            "       控制器型號, " +
                            "       使用期_月, " +
                            "       故障部件, " +
                            "       原因判定, " +
                            "       問題描述, " +
                            "       新代檢測說明, " +
                            "       故障品題分析 故障問題分析, " +
                            "       再發防止對策, " +
                            "       不良位置, " +                            
                            "       維修人員, " +
                            "       終端客戶, " +
                            "       機械廠, " +
                            "       CRM單號, " +
                            "       責任單位, " +
                            "       TypeComponents 組成料號 " +
                            "  FROM rptFixDetail " +
                            "  LEFT JOIN rptProductComponents ON (rptFixDetail.產品序號 = rptProductComponents.SN) " +
//                            " WHERE 維修單類別 LIKE '%2001' " +
                            " WHERE 使用期_月 >= 0 " +
                            "   AND 原因判定 NOT IN (" + FILTER_REASON + ") " +
                AddCriteria("   AND 維修日期 BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
                AddCriteria("   AND 出廠日期 BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
                AddCriteria("   AND 製造日期 BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
                AddCriteria("   AND 使用期_月 BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
                AddCriteria("   AND 使用期_月 = @UseTime", new string[] { "UseTime" }) +
                AddCriteria("   AND 維修單類別 IN (@TypeID)", new string[] { "TypeID" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 故障部件ID IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 產品分類 IN (@ProductClass)", new string[] { "ProductClass" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 產品品序 IN (@TypeSN)", new string[] { "TypeSN" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 責任單位ID IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 故障部件 IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 原因判定 IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 客戶簡稱 IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(4), 出廠日期, 111) IN (@YYYY)", new string[] { "YYYY" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') IN (@YYYYQQ)", new string[] { "YYYYQQ" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(7), 出廠日期, 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +                
                AddCriteria("   AND CONVERT(Varchar(4), 維修日期, 111) IN (@FixYYYY)", new string[] { "FixYYYY" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND (CAST(DATEPART(yyyy, 維修日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 維修日期) AS Varchar(2)) + 'Q') IN (@FixYYYYQQ)", new string[] { "FixYYYYQQ" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(7), 維修日期, 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(4), 製造日期, 111) IN (@CreateYYYY)", new string[] { "CreateYYYY" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND (CAST(DATEPART(yyyy, 製造日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 製造日期) AS Varchar(2)) + 'Q') IN (@CreateYYYYQQ)", new string[] { "CreateYYYYQQ" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(7), 製造日期, 111) IN (@CreateYYYYMM)", new string[] { "CreateYYYYMM" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 控制器型號 IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
                            " ORDER BY 叫修單號, 維修單號 ";
                AddSQLStatement(RMA_FIX_DETAIL, strSQL);
            

                   strSQL = "SELECT Convert(Varchar(7), 出廠日期, 111) YYYYMM, " +
                            "       使用期_月 UsePeriodMonth, " +
                            "       COUNT(DISTINCT 維修單號) RMACount " +
                            "  FROM rptFixDetail " +
//                            " WHERE 維修單類別 LIKE '%2001' " +
                            " WHERE 使用期_月 >= 0 " +
                            "   AND 原因判定 NOT IN (" + FILTER_REASON + ") " +
                AddCriteria("   AND 維修日期 BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
                AddCriteria("   AND 出廠日期 BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
                AddCriteria("   AND 製造日期 BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
                AddCriteria("   AND 使用期_月 BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
                AddCriteria("   AND 使用期_月 = @UseTime", new string[] { "UseTime" }) +
                AddCriteria("   AND 維修單類別 IN (@TypeID)", new string[] { "TypeID" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 故障部件ID IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 產品分類 IN (@ProductClass)", new string[] { "ProductClass" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 產品品序 IN (@TypeSN)", new string[] { "TypeSN" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 責任單位ID IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 故障部件 IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 原因判定 IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 客戶簡稱 IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(7), 出廠日期, 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') IN (@YYYYQQ)", new string[] { "YYYYQQ" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND CONVERT(Varchar(7), 維修日期, 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
                AddCriteria("   AND 控制器型號 IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
                            " GROUP BY Convert(Varchar(7), 出廠日期, 111), 使用期_月 " +
                            " ORDER BY YYYYMM, UsePeriodMonth ";

                AddSQLStatement(RMA_FIX_MONTHLY, strSQL);


                    strSQL = "SELECT Convert(Varchar(7), 出廠日期, 111) GroupBy, " +
                             "       故障部件 PartName, " +
                             "       COUNT(DISTINCT 維修單號) RMACount " +
                             "  FROM rptFixDetail " +
//                             " WHERE 維修單類別 LIKE '%2001' " +
                             " WHERE 使用期_月 >= 0 " +
                             "   AND 原因判定 NOT IN (" + FILTER_REASON + ") " +
                 AddCriteria("   AND 維修日期 BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
                 AddCriteria("   AND 出廠日期 BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
                 AddCriteria("   AND 製造日期 BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
                 AddCriteria("   AND 使用期_月 BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
                 AddCriteria("   AND 使用期_月 = @UseTime", new string[] { "UseTime" }) +
                 AddCriteria("   AND 維修單類別 IN (@TypeID)", new string[] { "TypeID" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 故障部件ID IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 產品分類 IN (@ProductClass)", new string[] { "ProductClass" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 產品品序 IN (@TypeSN)", new string[] { "TypeSN" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 責任單位ID IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 故障部件 IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 原因判定 IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 客戶簡稱 IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND CONVERT(Varchar(7), 出廠日期, 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') IN (@YYYYQQ)", new string[] { "YYYYQQ" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND CONVERT(Varchar(7), 維修日期, 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
                 AddCriteria("   AND 控制器型號 IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
                             " GROUP BY Convert(Varchar(7), 出廠日期, 111), 故障部件 " +
                             " ORDER BY GroupBy ASC, RMACount DESC";

                 AddSQLStatement(RMA_FIX_MONTHLY_BY_PART_TYPE, strSQL);


                     strSQL = "SELECT (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') GroupBy, " +
                              "       故障部件 PartName, " +
                              "       COUNT(DISTINCT 維修單號) RMACount " +
                              "  FROM rptFixDetail " +
//                              " WHERE 維修單類別 LIKE '%2001' " +
                              " WHERE 使用期_月 >= 0 " +
                              "   AND 原因判定 NOT IN (" + FILTER_REASON + ") " +
                  AddCriteria("   AND 維修日期 BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
                  AddCriteria("   AND 出廠日期 BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
                  AddCriteria("   AND 製造日期 BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
                  AddCriteria("   AND 使用期_月 BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
                  AddCriteria("   AND 使用期_月 = @UseTime", new string[] { "UseTime" }) +
                  AddCriteria("   AND 維修單類別 IN (@TypeID)", new string[] { "TypeID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 故障部件ID IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 產品分類 IN (@ProductClass)", new string[] { "ProductClass" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 產品品序 IN (@TypeSN)", new string[] { "TypeSN" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 責任單位ID IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 故障部件 IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 原因判定 IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 客戶簡稱 IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND CONVERT(Varchar(7), 出廠日期, 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') IN (@YYYYQQ)", new string[] { "YYYYQQ" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND CONVERT(Varchar(7), 維修日期, 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 控制器型號 IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
                              " GROUP BY (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q'), 故障部件 " +
                              " ORDER BY GroupBy ASC, RMACount DESC";

                 AddSQLStatement(RMA_FIX_QUOTERLY_BY_PART_TYPE, strSQL);

                     strSQL = "SELECT Convert(Varchar(7), 出廠日期, 111) YYYYMM, " +
                              "       故障部件 PartName, " +
                              "       原因判定 ReasonName, " +
                              "       COUNT(DISTINCT 維修單號) RMACount " +
                              "  FROM rptFixDetail " +
//                              " WHERE 維修單類別 LIKE '%2001' " +
                              " WHERE 使用期_月 >= 0 " +
                              "   AND 原因判定 NOT IN (" + FILTER_REASON + ") " +
                  AddCriteria("   AND 維修日期 BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
                  AddCriteria("   AND 出廠日期 BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
                  AddCriteria("   AND 製造日期 BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
                  AddCriteria("   AND 使用期_月 BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
                  AddCriteria("   AND 使用期_月 = @UseTime", new string[] { "UseTime" }) +
                  AddCriteria("   AND 維修單類別 IN (@TypeID)", new string[] { "TypeID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 故障部件ID IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 產品分類 IN (@ProductClass)", new string[] { "ProductClass" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 產品品序 IN (@TypeSN)", new string[] { "TypeSN" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 責任單位ID IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 故障部件 IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 原因判定 IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 客戶簡稱 IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND CONVERT(Varchar(7), 出廠日期, 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') IN (@YYYYQQ)", new string[] { "YYYYQQ" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND CONVERT(Varchar(7), 維修日期, 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 控制器型號 IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList) +
                              " GROUP BY Convert(Varchar(7), 出廠日期, 111), 故障部件, 原因判定 " +
                              " ORDER BY YYYYMM ASC, PartName, ReasonName ";

                  AddSQLStatement(RMA_FIX_MONTHLY_BY_PART_REASON, strSQL);


                     strSQL = "  FROM rptFixDetail " +
//                              " WHERE 維修單類別 LIKE '%2001' " +
                              " WHERE 使用期_月 >= 0 " +
                              "   AND 原因判定 NOT IN (" + FILTER_REASON + ") " +
                  AddCriteria("   AND 維修日期 BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
                  AddCriteria("   AND 出廠日期 BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
                  AddCriteria("   AND 製造日期 BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
                  AddCriteria("   AND 使用期_月 BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
                  AddCriteria("   AND 使用期_月 = @UseTime", new string[] { "UseTime" }) +
                  AddCriteria("   AND 維修單類別 IN (@TypeID)", new string[] { "TypeID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 故障部件ID IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 產品分類 IN (@ProductClass)", new string[] { "ProductClass" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 產品品序 IN (@TypeSN)", new string[] { "TypeSN" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 責任單位ID IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 故障部件 IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 原因判定 IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 客戶簡稱 IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND CONVERT(Varchar(7), 出廠日期, 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND (CAST(DATEPART(yyyy, 出廠日期) AS Varchar(4)) + '.' + CAST(DATEPART(qq, 出廠日期) AS Varchar(2)) + 'Q') IN (@YYYYQQ)", new string[] { "YYYYQQ" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND CONVERT(Varchar(7), 維修日期, 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
                  AddCriteria("   AND 控制器型號 IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList);

                  AddSQLStatement(RMA_FIX_GENERAL_GROUP_BY, strSQL);


            //string strWithAsSQL = " WITH ProductAttribute AS (SELECT a.TypeSN, a.TypeName, a.TypeSpec, b.ProductSN, ISNULL(a.MachineType, '未知') MachineType " +
            //                      "                             FROM sbcProductType a, sbcProductAttribute b " +
            //                      "                            WHERE a.TypeSN = b.MachineTypeSN), " +
            //                      "      ReasonTable AS (SELECT sbcRMAFixMaster.FixID,  sbcPartBreakReason.Name ReasonName " +
            //                      "                        FROM sbcRMAFixMaster, sbcRMAFixReason " +
            //                      "                        LEFT JOIN sbcPartBreakReason ON sbcRMAFixReason.ReasonID = sbcPartBreakReason.ID " +
            //                      "                       WHERE sbcRMAFixMaster.FixID = sbcRMAFixReason.RMAID " +
            //                      "                         AND sbcRMAFixReason.inUse = '1' " +
            //                      "                         AND sbcRMAFixMaster.FixID = sbcRMAFixReason.RMAID), " +
            //                      "      ShippingTable AS (SELECT a.ProductSN LastProductSN " +
            //                      "                              ,MAX(a.Cons_Date) LastConsDate " +
            //                      "                              ,MAX(b.Cons_Date) CreateDate " +
            //                      "                          FROM sbcHisMaster a, sbcProductMaster b " +
            //                      "                         WHERE HisTypeNo IN ('Y', 'L') " +
            //                      "                           AND a.ProductSN = b.SN " +
            //                      "                         GROUP BY a.ProductSN) ";            

            //string strFixDetailStatement = "select sbcRMAFixMaster.CallID 叫修單號, " +
            //                                "       sbcRMAFixMaster.FixID 維修單號, " +
            //                                "       Convert(Varchar(10), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) 出廠日期, " +
            //                                "       Convert(Varchar(10), ISNULL(ShippingTable.LastConsDate, '1900/01/01'), 111) 最後刷出廠日期, " +
            //                                "       Convert(Varchar(10), CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END, 111) 回推出廠日期, " +
            //                                "       ISNULL(Convert(Varchar(10), ShippingTable.CreateDate, 111), '1900/01/01') 製造日期, " +
            //                                "       ISNULL(Convert(Varchar(10), sbcRMACallMaster.CallDate, 111), '1900/01/01') 叫修日期, " +
            //                                "       ISNULL(Convert(Varchar(10), sbcRMAFixMaster.FixDate, 111), '1900/01/01') 維修日期, " +
            //                                "       sbcRMACallProductClass.Name 產品分類, " +
            //                                "       TEMP_CUSTOMER_NAME.SName 客戶簡稱  , " +
            //                                "       ProductAttribute.TypeSN 產品品序, " +
            //                                "       ProductAttribute.TypeName 產品品名, " +
            //                                "       ProductAttribute.TypeSpec 產品規格, " +
            //                                "       ProductAttribute.ProductSN 產品序號, " +
            //                                "       ProductAttribute.MachineType 控制器型號, " +
            //                                "       Question 問題描述, " +
            //                                "       ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) 使用期_月, " +
            //                                "       ReasonTable.ReasonName 原因判定, " +
            //                                "       (case when Explain is Null then Deal when Deal is null then Explain else Deal+Explain end) 新代檢測說明, " +
            //                                "       Analyze 故障品題分析, " +
            //                                "       Pervetion 再發防止對策, " +
            //                                "       BreadPos 不良位置, " +
            //                                h"       sbcPartType.Name 故障部件, " +
            //                                "       EmpName 維修人員, " +
            //                                "       sbcRMACallMaster.EndCustomer 終端客戶, " +
            //                                "       sbcRMACallMaster.MachineryFactory 機械廠, " +
            //                                "       sbcRMACallMaster.CRMTableNo CRM單號, " +
            //                                "       sbcRMAFixDutyUnit.Name 責任單位";
            //string strFixDetailFootStatement = "  ORDER BY sbcRMAFixMaster.CallID ";
            //AddGeneralSQL(RMA_FIX_DETAIL, strWithAsSQL);
            //AddGeneralSQL(RMA_FIX_DETAIL, strFixDetailStatement);
            //strSQL = " FROM sbcRMAFixMaster " +
            //         " LEFT JOIN sbcRMACallMaster ON sbcRMACallMaster.CallID = sbcRMAFixMaster.CallID " +
            //         " LEFT JOIN ProductAttribute ON sbcRMACallMaster.ProductSN = ProductAttribute.ProductSN " +
            //         " LEFT JOIN sbcRMACallProductClass on ProductClass = sbcRMACallProductClass.ID " +
            //         " LEFT JOIN ReasonTable ON sbcRMAFixMaster.FixID = ReasonTable.FixID " +
            //         " LEFT JOIN ShippingTable ON sbcRMACallMaster.ProductSN = ShippingTable.LastProductSN " +
            //         " LEFT JOIN TEMP_NAME ON sbcRMAFixMaster.FixUserID = TEMP_NAME.EmpID " +
            //         " LEFT JOIN TEMP_CUSTOMER_NAME ON sbcRMACallMaster.CustomerID = TEMP_CUSTOMER_NAME.Code " +
            //         " LEFT JOIN sbcPartType ON sbcRMAFixMaster.FixPart = sbcPartType.ID " +
            //         " LEFT JOIN sbcRMAFixDutyUnit ON sbcRMAFixMaster.DutyUnit = sbcRMAFixDutyUnit.ID " +
            //         "WHERE sbcRMAFixMaster.TypeID LIKE '%2001' " +
            //         AddCriteria("    AND sbcRMAFixMaster.FixDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END), 111) BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ShippingTable.CreateDate, 111) BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //         AddCriteria("    AND sbcRMACallMaster.UseTime BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
            //         AddCriteria("    AND sbcRMAFixMaster.FixPart IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcRMAFixMaster.DutyUnit IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcPartType.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ReasonTable.ReasonName IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND TEMP_CUSTOMER_NAME.SName IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END), 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(sbcRMAFixMaster.FixDate, '1900/01/01'), 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ProductAttribute.MachineType IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList);
            //AddSQLStatement(RMA_FIX_DETAIL, strSQL + " " + strFixDetailFootStatement);



            //string strFixMonthStatement = " SELECT COUNT(*) RMACount, " +
            //                              "        Convert(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) YYYYMM, " +
            //                              "        ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) UsePeriodMonth ";
            //string strFixMonthFootStatement = " GROUP BY CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) , ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) " +
            //                                  " ORDER BY YYYYMM ";
            //AddGeneralSQL(RMA_FIX_MONTHLY, strWithAsSQL);
            //AddGeneralSQL(RMA_FIX_MONTHLY, strFixMonthStatement);
            //strSQL = " FROM sbcRMAFixMaster " +
            //         " LEFT JOIN sbcRMACallMaster ON sbcRMACallMaster.CallID = sbcRMAFixMaster.CallID " +
            //         " LEFT JOIN ProductAttribute ON sbcRMACallMaster.ProductSN = ProductAttribute.ProductSN " +
            //         " LEFT JOIN ReasonTable ON sbcRMAFixMaster.FixID = ReasonTable.FixID " +
            //         " LEFT JOIN ShippingTable ON sbcRMACallMaster.ProductSN = ShippingTable.LastProductSN " +
            //         " LEFT JOIN TEMP_NAME ON sbcRMAFixMaster.FixUserID = TEMP_NAME.EmpID " +
            //         " LEFT JOIN TEMP_CUSTOMER_NAME ON sbcRMACallMaster.CustomerID = TEMP_CUSTOMER_NAME.Code " +
            //         " LEFT JOIN sbcPartType ON sbcRMAFixMaster.FixPart = sbcPartType.ID " +
            //         " LEFT JOIN sbcRMAFixDutyUnit ON sbcRMAFixMaster.DutyUnit = sbcRMAFixDutyUnit.ID " +
            //         "WHERE sbcRMAFixMaster.TypeID LIKE '%2001' " +
            //         AddCriteria("    AND sbcRMAFixMaster.FixDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END), 111) BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ShippingTable.CreateDate, 111) BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //         AddCriteria("    AND ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
            //         AddCriteria("    AND sbcRMAFixMaster.FixPart IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcRMAFixMaster.DutyUnit IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcPartType.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ReasonTable.ReasonName IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND TEMP_CUSTOMER_NAME.SName IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(sbcRMAFixMaster.FixDate, '1900/01/01'), 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ProductAttribute.MachineType IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList);
            //AddSQLStatement(RMA_FIX_MONTHLY, strSQL + " " + strFixMonthFootStatement);

            //string strFixMonthPartStatement = "SELECT COUNT(*) RMACount, " +
            //                                  "       CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) YYYYMM, " +
            //                                  "       sbcPartType.Name PartName ";
            //string strFixMonthPartFootStatement = " GROUP BY CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) , sbcPartType.Name " +
            //                                      " ORDER BY YYYYMM ASC, RMACount DESC";
            //AddGeneralSQL(RMA_FIX_MONTHLY_BY_PART_TYPE, strWithAsSQL);
            //AddGeneralSQL(RMA_FIX_MONTHLY_BY_PART_TYPE, strFixMonthPartStatement);
            //strSQL = " FROM sbcRMAFixMaster " +
            //         " LEFT JOIN sbcRMACallMaster ON sbcRMACallMaster.CallID = sbcRMAFixMaster.CallID " +
            //         " LEFT JOIN ProductAttribute ON sbcRMACallMaster.ProductSN = ProductAttribute.ProductSN " +
            //         " LEFT JOIN ReasonTable ON sbcRMAFixMaster.FixID = ReasonTable.FixID " +
            //         " LEFT JOIN ShippingTable ON sbcRMACallMaster.ProductSN = ShippingTable.LastProductSN " +
            //         " LEFT JOIN TEMP_NAME ON sbcRMAFixMaster.FixUserID = TEMP_NAME.EmpID " +
            //         " LEFT JOIN TEMP_CUSTOMER_NAME ON sbcRMACallMaster.CustomerID = TEMP_CUSTOMER_NAME.Code " +
            //         " LEFT JOIN sbcPartType ON sbcRMAFixMaster.FixPart = sbcPartType.ID " +
            //         " LEFT JOIN sbcRMAFixDutyUnit ON sbcRMAFixMaster.DutyUnit = sbcRMAFixDutyUnit.ID " +
            //         "WHERE sbcRMAFixMaster.TypeID LIKE '%2001' " +
            //         AddCriteria("    AND sbcRMAFixMaster.FixDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END), 111) BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ShippingTable.CreateDate, 111) BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //         AddCriteria("    AND ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
            //         AddCriteria("    AND sbcRMAFixMaster.FixPart IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcRMAFixMaster.DutyUnit IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcPartType.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ReasonTable.ReasonName IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND TEMP_CUSTOMER_NAME.SName IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(sbcRMAFixMaster.FixDate, '1900/01/01'), 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ProductAttribute.MachineType IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList);

            //AddSQLStatement(RMA_FIX_MONTHLY_BY_PART_TYPE, strSQL + " " + strFixMonthPartFootStatement);

            //string strFixMonthReasonStatement = "SELECT COUNT(*) RMACount, " +
            //                                  "         CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END), 111) YYYYMM, " +
            //                                  "         sbcPartType.Name PartName, " +
            //                                  "         ReasonTable.ReasonName ";
            //string strFixMonthReasonFootStatement = " GROUP BY CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN UseTime = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * UseTime, FixDate) END), 111) , sbcPartType.Name, ReasonTable.ReasonName " +
            //                                        " ORDER BY YYYYMM ASC, PartName, ReasonName ";
            //AddGeneralSQL(RMA_FIX_MONTHLY_BY_PART_REASON, strWithAsSQL);
            //AddGeneralSQL(RMA_FIX_MONTHLY_BY_PART_REASON, strFixMonthReasonStatement);
            //strSQL = " FROM sbcRMAFixMaster " +
            //         " LEFT JOIN sbcRMACallMaster ON sbcRMACallMaster.CallID = sbcRMAFixMaster.CallID " +
            //         " LEFT JOIN ProductAttribute ON sbcRMACallMaster.ProductSN = ProductAttribute.ProductSN " +
            //         " LEFT JOIN ReasonTable ON sbcRMAFixMaster.FixID = ReasonTable.FixID " +
            //         " LEFT JOIN ShippingTable ON sbcRMACallMaster.ProductSN = ShippingTable.LastProductSN " +
            //         " LEFT JOIN TEMP_NAME ON sbcRMAFixMaster.FixUserID = TEMP_NAME.EmpID " +
            //         " LEFT JOIN TEMP_CUSTOMER_NAME ON sbcRMACallMaster.CustomerID = TEMP_CUSTOMER_NAME.Code " +
            //         " LEFT JOIN sbcPartType ON sbcRMAFixMaster.FixPart = sbcPartType.ID " +
            //         " LEFT JOIN sbcRMAFixDutyUnit ON sbcRMAFixMaster.DutyUnit = sbcRMAFixDutyUnit.ID " +
            //         "WHERE sbcRMAFixMaster.TypeID LIKE '%2001' " +
            //         AddCriteria("    AND sbcRMAFixMaster.FixDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ShippingTable.CreateDate, 111) BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //         AddCriteria("    AND ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
            //         AddCriteria("    AND sbcRMAFixMaster.FixPart IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcRMAFixMaster.DutyUnit IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcPartType.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ReasonTable.ReasonName IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND TEMP_CUSTOMER_NAME.SName IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(sbcRMAFixMaster.FixDate, '1900/01/01'), 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ProductAttribute.MachineType IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList);

            //AddSQLStatement(RMA_FIX_MONTHLY_BY_PART_REASON, strSQL + " " + strFixMonthReasonFootStatement);


            //AddGeneralSQL(RMA_FIX_GENERAL_GROUP_BY, strWithAsSQL);
            //strSQL = " FROM sbcRMAFixMaster " +
            //         " LEFT JOIN sbcRMACallMaster ON sbcRMACallMaster.CallID = sbcRMAFixMaster.CallID " +
            //         " LEFT JOIN ProductAttribute ON sbcRMACallMaster.ProductSN = ProductAttribute.ProductSN " +
            //         " LEFT JOIN ReasonTable ON sbcRMAFixMaster.FixID = ReasonTable.FixID " +
            //         " LEFT JOIN ShippingTable ON sbcRMACallMaster.ProductSN = ShippingTable.LastProductSN " +
            //         " LEFT JOIN TEMP_NAME ON sbcRMAFixMaster.FixUserID = TEMP_NAME.EmpID " +
            //         " LEFT JOIN TEMP_CUSTOMER_NAME ON sbcRMACallMaster.CustomerID = TEMP_CUSTOMER_NAME.Code " +
            //         " LEFT JOIN sbcPartType ON sbcRMAFixMaster.FixPart = sbcPartType.ID " +
            //         " LEFT JOIN sbcRMAFixDutyUnit ON sbcRMAFixMaster.DutyUnit = sbcRMAFixDutyUnit.ID " +
            //         "WHERE sbcRMAFixMaster.TypeID LIKE '%2001' " +
            //         AddCriteria("    AND sbcRMAFixMaster.FixDate BETWEEN @StartDate AND @EndDate", new string[] { "StartDate", "EndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) BETWEEN @ShippingStartDate AND @ShippingEndDate", new string[] { "ShippingStartDate", "ShippingEndDate" }) +
            //         AddCriteria("    AND CONVERT(Varchar(10), ShippingTable.CreateDate, 111) BETWEEN @CreateStartDate AND @CreateEndDate", new string[] { "CreateStartDate", "CreateEndDate" }) +
            //         AddCriteria("    AND ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) BETWEEN @MinUseTime AND @MaxUseTime", new string[] { "MinUseTime", "MaxUseTime" }) +
            //         AddCriteria("    AND sbcRMAFixMaster.FixPart IN (@PartTypeID)", new string[] { "PartTypeID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcRMAFixMaster.DutyUnit IN (@DutyUnitID)", new string[] { "DutyUnitID" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND sbcPartType.Name IN (@PartName)", new string[] { "PartName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ReasonTable.ReasonName IN (@ReasonName)", new string[] { "ReasonName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND TEMP_CUSTOMER_NAME.SName IN (@CustomerName)", new string[] { "CustomerName" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(ShippingTable.LastConsDate, CASE WHEN ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime) = 9999 THEN '1900/01/01' ELSE DATEADD(mm, -1 * ISNULL(DateDiff(mm, ISNULL(ShippingTable.LastConsDate, ShippingTable.CreateDate), sbcRMAFixMaster.FixDate), UseTime), FixDate) END), 111) IN (@YYYYMM)", new string[] { "YYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND CONVERT(Varchar(7), ISNULL(sbcRMAFixMaster.FixDate, '1900/01/01'), 111) IN (@FixYYYYMM)", new string[] { "FixYYYYMM" }, CriteriaTypeEnum.InList) +
            //         AddCriteria("    AND ProductAttribute.MachineType IN (@MachineType)", new string[] { "MachineType" }, CriteriaTypeEnum.InList);
                     
            //         AddSQLStatement(RMA_FIX_GENERAL_GROUP_BY, strSQL);

        }
    }
}
