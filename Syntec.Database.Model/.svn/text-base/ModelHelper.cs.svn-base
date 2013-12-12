using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.Database.Model
{
    public class ModelHelper
    {
        public static long GetSeq(IDataModel dataModel, string strFormat, string strSeqFieldName)
        {
            List<IDataModel> lstConfig;
            long lSeq = -1;
            string strDateTimeFormat;
            string strSeqFormat;
            string strConfigId;

            try
            {
                strDateTimeFormat = strFormat.Substring(0, strFormat.IndexOf('x'));
                strSeqFormat = strFormat.Substring(0, strFormat.Length - 1).Replace('x', '0') + "1";

                lstConfig = (List<IDataModel>)dataModel.Query<IDataModel>("SELECT MAX(" + strSeqFieldName + ") LastConfigId FROM " + dataModel.TableName, null, null);
                if (lstConfig.Count > 0)
                {
                    strConfigId = lstConfig[0].Value("LastConfigId").ToString();

                    if (strConfigId.Trim() == "")
                    {
                        lSeq = long.Parse(DateTime.Now.ToString(strSeqFormat));
                    }
                    else if (strConfigId.Substring(0, strDateTimeFormat.Length) == DateTime.Now.ToString(strDateTimeFormat))
                    {
                        lSeq = long.Parse(strConfigId) + 1;
                    }
                    else
                    {
                        lSeq = long.Parse(DateTime.Now.ToString(strSeqFormat));
                    }

                }
                else
                {
                    lSeq = long.Parse(DateTime.Now.ToString(strSeqFormat));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lSeq;
        }
    }
}
