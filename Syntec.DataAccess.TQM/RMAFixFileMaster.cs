using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class RMAFixFileMaster : RMAAbstractDataAcces
    {
        public const int DETAIL = 1;
        public const int DETAIL_WITHOUT_PICTURE = 2;

        public RMAFixFileMaster()
        {
            string strSQL;

            strSQL = "SELECT ID, FixID, Filename, Memo, AttachFile, Uploader, UploadDate " +
                     "  FROM sbcRMAFixFileMaster " +
                     " WHERE 1 = 1 " +
         AddCriteria(" AND FixID = @FixID", new string[] { "FixID" }, CriteriaTypeEnum.Single) +
//         AddCriteria(" AND Filename = @Filename", new string[] { "Filename" }, CriteriaTypeEnum.Single) +
                     " UNION ALL " +
                     "SELECT ID, CallID FixID, Filename, Memo, AttachFile, Uploader, UploadDate " +
                     "  FROM sbcRMACallFileMaster " +
                     " WHERE 1 = 1 " +
         AddCriteria(" AND CallID = @CallID", new string[] { "CallID" }, CriteriaTypeEnum.Single);

            AddSQLStatement(DETAIL, strSQL);


            strSQL = "SELECT ID, FixID, Filename, Memo, Uploader, UploadDate " +
                     "  FROM sbcRMAFixFileMaster " +
                     " WHERE 1 = 1 " +
            AddCriteria(" AND FixID = @FixID", new string[] { "FixID" }, CriteriaTypeEnum.Single) +
//            AddCriteria(" AND Filename = @Filename", new string[] { "Filename" }, CriteriaTypeEnum.Single) +
                        " UNION ALL " +
                        "SELECT ID, CallID FixID, Filename, Memo, Uploader, UploadDate " +
                        "  FROM sbcRMACallFileMaster " +
                        " WHERE 1 = 1 " +
            AddCriteria(" AND CallID = @CallID", new string[] { "CallID" }, CriteriaTypeEnum.Single);
            AddSQLStatement(DETAIL_WITHOUT_PICTURE, strSQL);

        }

    }
}
