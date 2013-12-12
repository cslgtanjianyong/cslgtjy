using System;
using System.Collections.Generic;
using System.Text;

namespace Syntec.DataAccess.TQM
{
    public class ProductType : MappingTableDataAccess
    {
        public const int FILTER_WITH_TYPE_CLASS = 4;
        public ProductType()
            : base("TypeSN, TypeName", "sbcProductType")
        {
            string strSQL;

            strSQL = "SELECT TypeSN, TypeName " +
                     "  FROM sbcProductType " +
                     " WHERE 1 = 1 " +
                   AddCriteria(" AND TypeClass LIKE @TypeClass", new string[] { "TypeClass" }, CriteriaTypeEnum.Single) +
                   AddCriteria(" AND TypeSN LIKE @TypeSN", new string[] { "TypeSN" }, CriteriaTypeEnum.Single);
            AddSQLStatement(FILTER_WITH_TYPE_CLASS, strSQL);
        }
    }
}
