using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class ProductMasterDataAccess : RMAAbstractDataAcces
    {
        public const int DETAIL = 1;
        
        public ProductMasterDataAccess()
        {
            string strSQL;

            strSQL = "SELECT a.SN, a.TypeSN, b.MachineType, b.TypeName, a.InProductSN, a.Cons_Date, a.Cons_User, "
                   + "       a.ReplaceSN, a.SaleCompanyNow, a.SupplierCode, a.SaleSyntecNow, a.SaleCompanyNowID, a.TestStatus, "
                   + "       b.PartTypeID, c.Name PartName "
                   + "  FROM sbcProductMaster a, sbcProductType b, sbcPartType c " 
                   + " WHERE a.TypeSN = b.TypeSN "
                   + "   AND b.PartTypeID = c.ID "
                   + "   AND a.InProductSN IS NOT NULL "
                   + "   AND a.InProductSN <> '' "
         + AddCriteria(" AND a.Cons_Date >= @StartConsDate", new string[] { "StartConsDate" })
         + AddCriteria(" AND a.SN = @ProductSN", new string[] { "ProductSN" })
         + AddCriteria(" AND a.InProductSN = @InProductSN", new string[] { "InProductSN" })
                   + " ORDER BY InProductSN ";
            AddSQLStatement(DETAIL, strSQL);
        }
    }
}
