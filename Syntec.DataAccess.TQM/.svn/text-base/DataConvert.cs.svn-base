using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Syntec.DataAccess.TQM
{
    public class DataConvert
    {
        public const int START = 0;
        public const int END = 1;

        //public static readonly int[] USE_PERIOD_UBOUND = new int[] { 3, 6, 9, 12, 18, 30, 47, 59, 71, 83, 95, 107, 119, 9999 };
        public static readonly int[] USE_PERIOD_UBOUND = new int[] { 3, 6, 9, 12, 18, 30, 47, 9999 };        

        public static string SeqFromDate(string strDate, int iType)
        {
            strDate = (strDate == "" || strDate == null) 
                        ?   iType == START
                                ?   "1900/01/01"
                                :   "2099/12/31"
                        :   strDate;
            return DateTime.Parse(strDate).ToString("yyyyMMdd") + (iType == START ? "000" : "999");
        }

        public static void InitDataTable(DataTable dataTable)
        {
            InitDataTable(dataTable, USE_PERIOD_UBOUND);
        }

        public static void InitDataTable(DataTable dataTable, int[] iUsePeriodUBounds)
        {
            if (iUsePeriodUBounds == null)
                iUsePeriodUBounds = USE_PERIOD_UBOUND;
            dataTable.Columns.Add("YearMonth", typeof(string));
            dataTable.Columns.Add("YearQuarter", typeof(string));
            dataTable.Columns.Add("ShippingMonthly", typeof(string));
            dataTable.Columns.Add("ShippingQuarterly", typeof(string));
            for (int i = 0; i < iUsePeriodUBounds.Length; i++)
            {
                dataTable.Columns.Add("DefectCountMonthly" + i, typeof(string));
                dataTable.Columns.Add("DefectRateMonthly" + i, typeof(string));
                dataTable.Columns.Add("DefectRateQuarterly" + i, typeof(string));
            }

            dataTable.Columns.Add("DefectCountMonthly9999", typeof(string));
            dataTable.Columns.Add("DefectRateMonthly9999", typeof(string));
            dataTable.Columns.Add("DefectRateQuarterly9999", typeof(string));
        }

        public static int PeriodIndexFromUseMonth(int iUseMonth)
        {
            return PeriodIndexFromUseMonth(iUseMonth, USE_PERIOD_UBOUND);
        }

        public static int PeriodIndexFromUseMonth(int iUseMonth, int[] iUsePeriodUBounds)
        {
            if (iUsePeriodUBounds == null)
                iUsePeriodUBounds = USE_PERIOD_UBOUND;
            int iIndex = 0;
            while ((iUseMonth > iUsePeriodUBounds[iIndex]) && (iIndex < iUsePeriodUBounds.Length))
                iIndex++;

            return iIndex;
        }
        
        public static DataRow InitRow(DataTable quarterSummary)
        {
            return InitRow(quarterSummary, USE_PERIOD_UBOUND);
        }

        public static DataRow InitRow(DataTable quarterSummary, int[] iUsePeriodUBounds)
        {
            if (iUsePeriodUBounds == null)
                iUsePeriodUBounds = USE_PERIOD_UBOUND;
            DataRow dataRow = quarterSummary.NewRow();
            dataRow["YearMonth"] = "";
            dataRow["YearQuarter"] = "";
            dataRow["ShippingMonthly"] = "0";
            dataRow["ShippingQuarterly"] = "0";
            for (int j = 0; j < iUsePeriodUBounds.Length; j++)
            {
                dataRow["DefectCountMonthly" + j] = "0";
                dataRow["DefectRateMonthly" + j] = "0.00%";
                dataRow["DefectRateQuarterly" + j] = "0.00%";
            }

            dataRow["DefectCountMonthly9999"] = "0";
            dataRow["DefectRateMonthly9999"] = "0.00%";
            dataRow["DefectRateQuarterly9999"] = "0.00%";

            quarterSummary.Rows.Add(dataRow);
            return dataRow;
        }
    }
}
