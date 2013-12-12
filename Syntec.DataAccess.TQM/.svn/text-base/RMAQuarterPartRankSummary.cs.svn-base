using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Syntec.Base;

namespace Syntec.DataAccess.TQM
{
    public class RMAQuarterPartRankSummary : RMAAbstractDataAcces
    {
        public const int BY_MONTH = 1;
        public const int BY_QUOTER = 2;

        private const int RANK_COUNT = 5;
        private readonly string[] RANK_NAME = new string[] { "FirstRank", "SecondRank", "ThirdRank", "FourthRank", "FifthRank"};

        //初始化排名表
        private void InitDataTable(DataTable dataTable)
        {
            dataTable.Columns.Add("GroupBy", typeof(string));
            for(int i = 0; i < RANK_NAME.Length; i++)
                dataTable.Columns.Add(RANK_NAME[i], typeof(string));
        }

        //初始化各季詳表
        private void InitDetailTable(DataTable dataTable)
        {            
            dataTable.Columns.Add("PartName", typeof(string));
            dataTable.Columns.Add("RMACount", typeof(int));
        }
        
        //將詳表轉換至排名表
        private void CreateDataRow(Dictionary<string, int> dicQuarter, DataTable quarterSummary, string strYearQuarter)
        {
            //季部件排名表
            DataTable quarterDetail;
            DataRow partDataRow;
            DataRow countDataRow;
            DataRow dataRow;

            quarterDetail = new DataTable();
            InitDetailTable(quarterDetail);

            foreach (string strPartName in dicQuarter.Keys)
            {
                dataRow = quarterDetail.NewRow();
                dataRow["PartName"] = strPartName;
                dataRow["RMACount"] = dicQuarter[strPartName];
                quarterDetail.Rows.Add(dataRow);
            }

            //依個數降冪排名
            quarterDetail.DefaultView.Sort = "RMACount DESC";
            quarterDetail = quarterDetail.DefaultView.ToTable();

            partDataRow = quarterSummary.NewRow();
            countDataRow = quarterSummary.NewRow();

            //將前N名填至部件名稱及維修個數
            partDataRow["GroupBy"] = strYearQuarter;
            countDataRow["GroupBy"] = strYearQuarter;

            for (int j = 0; j < Math.Min(quarterDetail.Rows.Count, RANK_COUNT); j++)
            {
                partDataRow[RANK_NAME[j]] = quarterDetail.Rows[j]["PartName"];
                countDataRow[RANK_NAME[j]] = quarterDetail.Rows[j]["RMACount"].ToString();
            }

            quarterSummary.Rows.Add(partDataRow);
            quarterSummary.Rows.Add(countDataRow);
        }

        public override bool ExecuteCommand(object oDataSet)
        {
            //取得RMA總表資料
            RMADetailDataAccess rmaDetail = new RMADetailDataAccess();
            rmaDetail.AddParam(GetAllParam());
            //依月查詢維修部件個數
            if (rmaDetail.Execute(RMADetailDataAccess.RMA_FIX_MONTHLY_BY_PART_TYPE) == false)
            {
                ErrorMessage = rmaDetail.ErrorMessage;
                return false;
            }

            DataTable monthlyTable = rmaDetail.GetData(RMADetailDataAccess.RMA_FIX_MONTHLY_BY_PART_TYPE);
            //季統計報表
            DataTable quarterSummary = new DataTable();          

            Dictionary<string, int> dicQuarter = new Dictionary<string,int>();

            string strPreviousYearQuarter = "1900.01";
            string strYearQuarter;

            try
            {
                switch((int)oDataSet)
                {
                    case BY_QUOTER:
                        strPreviousYearQuarter = "1900.1Q";
                        break;
                    default:
                        strPreviousYearQuarter = "1900.01";
                        break;
                }

                InitDataTable(quarterSummary);
                for (int i = 0; i < monthlyTable.Rows.Count; i++)
                {
                    switch((int)oDataSet)
                    {
                        case BY_QUOTER:
                            strYearQuarter = DateHelper.QuoterFromMonth(monthlyTable.Rows[i]["GroupBy"].ToString());
                            break;
                        default:
                            strYearQuarter = monthlyTable.Rows[i]["GroupBy"].ToString();
                            break;
                    }
                    if ((strYearQuarter != strPreviousYearQuarter) && (dicQuarter.Count > 0))
                    {
                        //產生季排名列
                        CreateDataRow(dicQuarter, quarterSummary, strPreviousYearQuarter);
                        //將季資料清空
                        dicQuarter = new Dictionary<string, int>();
                    }

                    strPreviousYearQuarter = strYearQuarter;
                    if (!dicQuarter.ContainsKey(monthlyTable.Rows[i]["PartName"].ToString()))
                        dicQuarter.Add(monthlyTable.Rows[i]["PartName"].ToString(), 0);
                    dicQuarter[monthlyTable.Rows[i]["PartName"].ToString()] += int.Parse(monthlyTable.Rows[i]["RMACount"].ToString());

                }

                if(dicQuarter.Count > 0)
                    CreateDataRow(dicQuarter, quarterSummary, strPreviousYearQuarter);                    

                AddData(quarterSummary);
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message + "\n" + ex.StackTrace;
            }
            return false;

        }

    }
}
