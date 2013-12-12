using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Syntec.Base;

namespace Syntec.DataAccess.TQM
{
    public class RMADefectRatioSummary : RMAAbstractDataAcces
    {
        public const string USE_PERIOD_UBOUND = "UsePeriodUBound";

        public override bool ExecuteCommand(object oDataSet)
        {
            DateTime dtTime1 = DateTime.Now;
            //取得RMA總表資料
            RMADetailDataAccess rmaDetail = new RMADetailDataAccess();
            rmaDetail.AddParam(GetAllParam());
            //維修單號需將日期轉成序號
            //rmaDetail.AddParam("StartDate", DataConvert.SeqFromDate(GetParam("StartDate").ToString(), DataConvert.START));
            //rmaDetail.AddParam("EndDate", DataConvert.SeqFromDate(GetParam("EndDate").ToString(), DataConvert.END));
            if (rmaDetail.Execute(RMADetailDataAccess.RMA_FIX_MONTHLY) == false)
            {
                ErrorMessage = rmaDetail.ErrorMessage;
                return false;
            }
            DateTime dtTime2 = DateTime.Now;
            //取得月/季出貨資料
            ShippingDataAccess monthlyShipping = new ShippingDataAccess();
            monthlyShipping.AddParam(GetAllParam());
            if (monthlyShipping.Execute(ShippingDataAccess.SHIPPING_MONTHLY
                                      + ShippingDataAccess.SHIPPING_QUARTERLY) == false)
            {
                ErrorMessage = monthlyShipping.ErrorMessage;
                return false;
            }
            DateTime dtTime3 = DateTime.Now;
            //月出貨量
            Dictionary<string, int> dicMonthlyShipping = new Dictionary<string, int>();
            //季出貨量
            Dictionary<string, int> dicQuarterlyShipping = new Dictionary<string, int>();
            //出貨月份的RowIndex
            Dictionary<string, int> dicMonthIndex = new Dictionary<string, int>();
            //季維修量 (區分使用期_月)
            Dictionary<string, int> dicQuarterCount = new Dictionary<string, int>();

            DataTable detailTable = rmaDetail.GetData(RMADetailDataAccess.RMA_FIX_MONTHLY);
            DataTable monthlyShippingTable = monthlyShipping.GetData(ShippingDataAccess.SHIPPING_MONTHLY);
            DataTable quarterlyShippingTable = monthlyShipping.GetData(ShippingDataAccess.SHIPPING_QUARTERLY);
            DataTable quarterSummary = new DataTable();

            //維修品出貨月份
            string strShippingMonth = "";
            //維修品出貨季份
            string strShippingQuarter = "";
            //使用期-月
            string strUseMonth = "";

            //月出貨量
            int iMonthlyShipping = 0;
            //季出貨量
            int iQuarterlyShipping = 0;
            //前一個年季內容
            string strPreviousQuarter = "";

            int iUseTime = -1;
            int iPeriodIndex = -1;
            DataRow dataRow;
            List<DataRow> lstInsertRow;
            List<int> lstInsertRowIndex;
            DateTime dtCurrentDate = DateHelper.Parse(GetParam("ShippingStartDate").ToString());
            string strEndMonth = DateHelper.Parse(GetParam("ShippingEndDate").ToString()).ToString("yyyyMM");
            int[] iUsePeriodUBounds = GetConfigParam(USE_PERIOD_UBOUND) == null ? DataConvert.USE_PERIOD_UBOUND : (int[])GetConfigParam(USE_PERIOD_UBOUND);

            try
            {

                DataConvert.InitDataTable(quarterSummary, iUsePeriodUBounds);

                while (dtCurrentDate.ToString("yyyyMM").CompareTo(strEndMonth) <= 0)
                {
                    strShippingMonth = dtCurrentDate.ToString("yyyy/MM");
                    strShippingQuarter = DateHelper.QuoterFromMonth(strShippingMonth);

                    dicMonthIndex.Add(strShippingMonth, quarterSummary.Rows.Count);
                    dataRow = DataConvert.InitRow(quarterSummary, iUsePeriodUBounds);
                    dataRow["YearMonth"] = strShippingMonth;
                    dataRow["YearQuarter"] = strShippingQuarter;
                    dtCurrentDate = dtCurrentDate.AddMonths(1);
                }

                DateTime dtTime4 = DateTime.Now;

                for (int i = 0; i < quarterlyShippingTable.Rows.Count; i++)
                {
                    if (!dicQuarterlyShipping.ContainsKey(quarterlyShippingTable.Rows[i]["YYYYQQ"].ToString()))
                        dicQuarterlyShipping.Add(quarterlyShippingTable.Rows[i]["YYYYQQ"].ToString(), 0);
                    dicQuarterlyShipping[quarterlyShippingTable.Rows[i]["YYYYQQ"].ToString()] = int.Parse(quarterlyShippingTable.Rows[i]["ShippingCount"].ToString());
                }

                DateTime dtTime5 = DateTime.Now;

                for (int i = 0; i < monthlyShippingTable.Rows.Count; i++)
                {
                    strShippingMonth = monthlyShippingTable.Rows[i]["YYYYMM"].ToString();
                    strShippingQuarter = DateHelper.QuoterFromMonth(strShippingMonth);

                    if (dicMonthIndex.ContainsKey(strShippingMonth) == false)
                        continue;
                    quarterSummary.Rows[dicMonthIndex[strShippingMonth]]["ShippingMonthly"] = monthlyShippingTable.Rows[i]["ShippingCount"].ToString();
                    quarterSummary.Rows[dicMonthIndex[strShippingMonth]]["ShippingQuarterly"] = dicQuarterlyShipping.ContainsKey(strShippingQuarter) ? dicQuarterlyShipping[strShippingQuarter].ToString() : "0";

                    dicMonthlyShipping[strShippingMonth] = int.Parse(monthlyShippingTable.Rows[i]["ShippingCount"].ToString());
                }

                DateTime dtTime6 = DateTime.Now;

                for (int i = 0; i < detailTable.Rows.Count; i++)
                {
                    strShippingMonth = detailTable.Rows[i]["YYYYMM"].ToString();

                    strUseMonth = detailTable.Rows[i]["UsePeriodMonth"].ToString();
                    iUseTime = int.Parse(strUseMonth == "" ? "0" : strUseMonth);
                    iPeriodIndex = DataConvert.PeriodIndexFromUseMonth(iUseTime, iUsePeriodUBounds);
                    strShippingQuarter = DateHelper.QuoterFromMonth(strShippingMonth);

                    if (iPeriodIndex < 0)
                        continue;

                    if (!dicMonthIndex.ContainsKey(strShippingMonth))
                    {
                        // need to process new row
                        continue;
                    }

                    string strQuarterSummary = quarterSummary.Rows[dicMonthIndex[strShippingMonth]]["DefectCountMonthly" + iPeriodIndex].ToString();
                    string strQuarterRMA = detailTable.Rows[i]["RMACount"].ToString();

                    quarterSummary.Rows[dicMonthIndex[strShippingMonth]]["DefectCountMonthly" + iPeriodIndex] =
                                                                                (int.Parse(quarterSummary.Rows[dicMonthIndex[strShippingMonth]]["DefectCountMonthly" + iPeriodIndex].ToString()) +
                                                                                 int.Parse(detailTable.Rows[i]["RMACount"].ToString())).ToString();

                    if (!dicQuarterCount.ContainsKey(strShippingQuarter + "_" + iPeriodIndex))
                        dicQuarterCount.Add(strShippingQuarter + "_" + iPeriodIndex, 0);
                    dicQuarterCount[strShippingQuarter + "_" + iPeriodIndex] += int.Parse(detailTable.Rows[i]["RMACount"].ToString());
                }
                DateTime dtTime7 = DateTime.Now;

                lstInsertRow = new List<DataRow>();
                lstInsertRowIndex = new List<int>();
                for (int i = 0; i < quarterSummary.Rows.Count; i++)
                {
                    iQuarterlyShipping = dicQuarterlyShipping.ContainsKey(quarterSummary.Rows[i]["YearQuarter"].ToString())
                                                ? dicQuarterlyShipping[quarterSummary.Rows[i]["YearQuarter"].ToString()]
                                                : 0;

                    iMonthlyShipping = dicMonthlyShipping.ContainsKey(quarterSummary.Rows[i]["YearMonth"].ToString())
                        ? dicMonthlyShipping[quarterSummary.Rows[i]["YearMonth"].ToString()]
                        : 0;

                    quarterSummary.Rows[i]["ShippingQuarterly"] = iQuarterlyShipping.ToString();

                    for (int j = 0; j <= iUsePeriodUBounds.Length; j++)
                    {
                        if (j == iUsePeriodUBounds.Length)
                            j = 9999;

                        if (!dicQuarterCount.ContainsKey(quarterSummary.Rows[i]["YearQuarter"].ToString() + "_" + j))
                            continue;

                        quarterSummary.Rows[i]["DefectRateMonthly" + j] = iMonthlyShipping == 0
                                                                            ? "0.00%"
                                                                            : (double.Parse(quarterSummary.Rows[i]["DefectCountMonthly" + j].ToString()) /
                                                                                iMonthlyShipping * 100.0).ToString("0.00") + "%";
                        quarterSummary.Rows[i]["DefectRateQuarterly" + j] = iQuarterlyShipping == 0
                                                                                ? "0.00%"
                                                                                : (double.Parse(dicQuarterCount[quarterSummary.Rows[i]["YearQuarter"].ToString() + "_" + j].ToString()) /
                                                                                    iQuarterlyShipping * 100.0).ToString("0.00") + "%";
                    }

                    if ((quarterSummary.Rows[i]["YearQuarter"].ToString() != strPreviousQuarter) &&
                        (strPreviousQuarter != ""))
                    {
                        dataRow = CreateRow(quarterSummary, i - 1);
                        lstInsertRow.Add(dataRow);
                        lstInsertRowIndex.Add(i);
                    }
                    strPreviousQuarter = quarterSummary.Rows[i]["YearQuarter"].ToString();
                }

                DateTime dtTime8 = DateTime.Now;

                dataRow = CreateRow(quarterSummary, quarterSummary.Rows.Count - 1);
                lstInsertRow.Add(dataRow);
                lstInsertRowIndex.Add(quarterSummary.Rows.Count);

                for (int i = lstInsertRow.Count - 1; i >= 0; i--)
                {
                    quarterSummary.Rows.InsertAt(lstInsertRow[i], lstInsertRowIndex[i]);
                }

                DateTime dtTime9 = DateTime.Now;
                AddData(quarterSummary);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + "\n" + ex.StackTrace + "\n" + strUseMonth;
            }
            return false;

        }

        private DataRow CreateRow(DataTable quarterSummary, int iRowIndex)
        {
            //前一季不良率
            float fDefectRatePreviousQuarter;
            //當季不良率
            float fDefectRateQuarter;
            int[] iUsePeriodUBounds = GetConfigParam(USE_PERIOD_UBOUND) == null ? DataConvert.USE_PERIOD_UBOUND : (int[])GetConfigParam(USE_PERIOD_UBOUND);

            DataRow dataRow = quarterSummary.NewRow();
            dataRow["YearMonth"] = "";
            dataRow["YearQuarter"] = "";
            dataRow["ShippingMonthly"] = "";
            dataRow["ShippingQuarterly"] = "累計不良率";
            dataRow["DefectCountMonthly0"] = "";
            dataRow["DefectRateMonthly0"] = "";
            dataRow["DefectRateQuarterly0"] = quarterSummary.Rows[iRowIndex]["DefectRateQuarterly0"].ToString();
            for (int j = 1; j <= iUsePeriodUBounds.Length; j++)
            {
                fDefectRatePreviousQuarter = NumericHelper.ParseFloat(dataRow["DefectRateQuarterly" + (j - 1)].ToString().Replace("%", ""));

                if (j == iUsePeriodUBounds.Length)
                    j = 9999;
                fDefectRateQuarter = NumericHelper.ParseFloat(quarterSummary.Rows[iRowIndex]["DefectRateQuarterly" + j].ToString().Replace("%", ""));

                dataRow["DefectCountMonthly" + j] = "";
                dataRow["DefectRateMonthly" + j] = "";
                dataRow["DefectRateQuarterly" + j] = (float.IsNaN(fDefectRatePreviousQuarter) || float.IsNaN(fDefectRateQuarter))
                                                        ? "0.00%"
                                                        : (fDefectRateQuarter + fDefectRatePreviousQuarter).ToString("0.00") + "%";
            }

            return dataRow;
        }
    }
}
