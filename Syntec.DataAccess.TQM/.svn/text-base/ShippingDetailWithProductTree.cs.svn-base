using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class ShippingDetailWithProductTree : RMAAbstractDataAcces
    {
        public const int SHIPPING_DETAIL = 1;
        public const int GENERAL_SHIPPING_COUNT = 2;

        public ShippingDetailWithProductTree()
        {
            string strSQL;

            strSQL = "SELECT ShippingType, LastConsDate ShippingDate, CreateDate, "
                   + "       SN1, TypeSN1, PartTypeID1, Name1,  "
                   + "       SN2, TypeSN2, PartTypeID2, Name2,  "
                   + "       SN3, TypeSN3, PartTypeID3, Name3,  "
                   + "       SN4, TypeSN4, PartTypeID4, Name4,  "
                   + "       SN5, TypeSN5, PartTypeID5, Name5  "
                   + "  FROM rptProductTree "
                   + "  LEFT JOIN rptShippingDetail ON rptProductTree.SN1 = rptShippingDetail.ProductSN "
                   + " WHERE 1 = 1 "
            + AddCriteria(" AND (TypeSN1 IN (@TypeSN) OR TypeSN2 IN (@TypeSN) OR TypeSN3 IN (@TypeSN) OR TypeSN4 IN (@TypeSN) OR TypeSN5 IN (@TypeSN) ", new string[] { "TypeSN" }, CriteriaTypeEnum.InList)
            + AddCriteria(" AND LastConsDate BETWEEN @StartShippingDate AND @EndShippingDate ", new string[] {"StartShippingDate", "EndShippingDate"}) 
            + AddCriteria(" AND CreateDate BETWEEN @StartCreateDate AND @EndCreateDate ", new string[] {"StartCreateDate", "EndCreateDate"}) 
            + AddCriteria(" AND ShippingType IN (@ShippingType) ", new string[] {"ShippingType"}, CriteriaTypeEnum.InList);

            AddSQLStatement(SHIPPING_DETAIL, strSQL);


            strSQL = "  FROM rptProductTree, rptShippingDetail "
                   + " WHERE rptProductTree.SN1 = rptShippingDetail.ProductSN "
            + AddCriteria(" AND (TypeSN1 IN (@TypeSN) OR TypeSN2 IN (@TypeSN) OR TypeSN3 IN (@TypeSN) OR TypeSN4 IN (@TypeSN) OR TypeSN5 IN (@TypeSN) ", new string[] { "TypeSN" }, CriteriaTypeEnum.InList)
            + AddCriteria(" AND LastConsDate BETWEEN @StartShippingDate AND @EndShippingDate ", new string[] { "StartShippingDate", "EndShippingDate" })
            + AddCriteria(" AND CreateDate BETWEEN @StartCreateDate AND @EndCreateDate ", new string[] { "StartCreateDate", "EndCreateDate" })
            + AddCriteria(" AND ShippingType IN (@ShippingType) ", new string[] { "ShippingType" }, CriteriaTypeEnum.InList);

            AddSQLStatement(GENERAL_SHIPPING_COUNT, strSQL);
        }
    }
}
