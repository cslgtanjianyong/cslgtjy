using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Syntec.Base;
using Syntec.DataTransfer;

namespace Syntec.DataAccess.TQM
{
    public class RMAGeneralRankDataAccess : RMAAbstractDataAcces
    {
        public const int GENERAL = RMADetailDataAccess.RMA_FIX_GENERAL_GROUP_BY;

        public override bool ExecuteCommand(object oDataSet)
        {
            //取得RMA總表資料
            RMADetailDataAccess rmaDetail = new RMADetailDataAccess();
            rmaDetail.AddParam(GetAllParam());            
            //rmaDetail.AddGroupBy(oDataSet, "CONVERT(Varchar(7), DATEADD(mm, -1 * UseTime, FixDate), 111)", "YYYYMM");            
            rmaDetail.AddGroupBy(oDataSet, GetAllGroupBy(oDataSet));
            rmaDetail.AddSummaryField(oDataSet, GetAllSummaryField(oDataSet));

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
                AddData(oDataSet, monthlyTable);
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
