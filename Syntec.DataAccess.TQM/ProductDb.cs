using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class ProductDb : RMAAbstractDataAcces
    {
        public const int DETAIL = 1;

        public ProductDb()
        {
            string strSQL;

            strSQL = "SELECT ProductSN, ShippingCount, CreateTime, CreateUser, TWShippingTime, TWShippingDest, TWShippingUser, "
                   + "       CustomerShippingTime, CustomerShippingSource, CustomerShippingDest, CustomerShippingUser, "
                   + "       CurrentHisTypeNo, CurrentPosition, MachineType, TypeName, TypeSN, BoxNo, "
                   + "       RootProductSN, RootTypeSN, RootTypeName, RootMachineType, IsRoot,  ParentProductSN, ChildrenProductSN, "
                   + "       PartTypeID, PartTypeName "
                   + "  FROM rptProductDb "
                   + " WHERE 1 = 1 "
         + AddCriteria(" AND ProductSN = @ProductSN", new string[] { "ProductSN" })
         + AddCriteria(" AND ShippingCount = @ShippingCount", new string[] { "ShippingCount" })
         + AddCriteria(" AND ShippingCount BETWEEN @StartShippingCount AND @EndShippingCount", new string[] { "StartShippingCount", "EndShippingCount" });
            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
