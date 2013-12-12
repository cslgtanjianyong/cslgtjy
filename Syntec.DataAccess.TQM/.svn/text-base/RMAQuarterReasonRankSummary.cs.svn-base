using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Syntec.Base;
using Syntec.DataTransfer;

namespace Syntec.DataAccess.TQM
{
    public class RMAQuarterReasonRankSummary : RMAAbstractDataAcces
    {
        public const int REASON = RMADetailDataAccess.RMA_FIX_MONTHLY_BY_PART_REASON;
        public const int PART_MONTH = RMADetailDataAccess.RMA_FIX_MONTHLY_BY_PART_TYPE;
        public const int PART_QUOTER = RMADetailDataAccess.RMA_FIX_QUOTERLY_BY_PART_TYPE;
        
        public override bool ExecuteCommand(object oDataSet)
        {
            //取得RMA總表資料
            RMADetailDataAccess rmaDetail = new RMADetailDataAccess();
            rmaDetail.AddParam(GetAllParam());
            //依月查詢維修部件個數
            if (rmaDetail.Execute(oDataSet) == false)
            {
                ErrorMessage = rmaDetail.ErrorMessage;
                return false;
            }

            DataTable monthlyTable = rmaDetail.GetData(oDataSet);

            DataTable quarterlyTable = null;
            Dictionary<string, IGroupAction> dicGroupAction;

            try
            {
                //quarterlyTable = TransferHelper.Group(monthlyTable, new List<string>(new string[] { "YYYYMM", "PartName", "ReasonName" }), new List<ITranslate>(new ITranslate[] { new MonthToQuarterTranslate(), new PassTranslate(), new PassTranslate() }), DataTypeEnum.Int);
                switch ((int)oDataSet)
                {
                    case PART_MONTH:
                        //dicGroupAction = new Dictionary<string, IGroupAction>();
                        //dicGroupAction.Add("ReasonName", new StringGroupAction(","));
                        //dicGroupAction.Add("RMACount", new NumericGroupAction());
                        //quarterlyTable = TransferHelper.Group(monthlyTable, new List<string>(new string[] { "YYYYMM", "PartName" }), new List<ITranslate>(new ITranslate[] { new MonthToQuarterTranslate(), new PassTranslate() }), DataTypeEnum.Int);
                        //AddData(oDataSet, quarterlyTable);
                        AddData(oDataSet, monthlyTable);
                        break;
                    case PART_QUOTER:
                        //dicGroupAction = new Dictionary<string, IGroupAction>();
                        //dicGroupAction.Add("ReasonName", new StringGroupAction(","));
                        //dicGroupAction.Add("RMACount", new NumericGroupAction());
                        //quarterlyTable = TransferHelper.Group(monthlyTable, new List<string>(new string[] { "YYYYMM", "PartName" }), new List<ITranslate>(new ITranslate[] { new MonthToQuarterTranslate(), new PassTranslate() }), DataTypeEnum.Int);
                        //AddData(oDataSet, quarterlyTable);
                        AddData(oDataSet, monthlyTable);
                        break;
                    case REASON:
                        AddData(oDataSet, monthlyTable);
                        //dicGroupAction = new Dictionary<string, IGroupAction>();
                        //dicGroupAction.Add("ReasonName", new StringGroupAction(","));
                        //dicGroupAction.Add("RMACount", new NumericGroupAction());
                        //quarterlyTable = TransferHelper.Group(monthlyTable, new List<string>(new string[] { "YYYYMM", "PartName" }), new List<ITranslate>(new ITranslate[] { new PassTranslate(), new PassTranslate() }), dicGroupAction);
                        //AddData(oDataSet, quarterlyTable);
                        break;
                }
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
