using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace syntec.DAL
{
	/// <summary>
	/// 数据访问类:ExcelRule
	/// </summary>
	public partial class ExcelRule
	{
		public ExcelRule()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "ExcelRule"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from ExcelRule");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(syntec.Model.ExcelRule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into ExcelRule(");
			strSql.Append("reportSource,sheetName,position,symbol,aim,planType,timeType,timeSpace,everydayFreSpace,startTime,endTime,startDate,endDate,email,article,attachmentSource,nextDoTimeDate)");
			strSql.Append(" values (");
			strSql.Append("@reportSource,@sheetName,@position,@symbol,@aim,@planType,@timeType,@timeSpace,@everydayFreSpace,@startTime,@endTime,@startDate,@endDate,@email,@article,@attachmentSource,@nextDoTimeDate)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@reportSource", SqlDbType.NVarChar,100),
					new SqlParameter("@sheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@position", SqlDbType.VarChar,3),
					new SqlParameter("@symbol", SqlDbType.NVarChar,3),
					new SqlParameter("@aim", SqlDbType.NVarChar,50),
					new SqlParameter("@planType", SqlDbType.NChar,10),
					new SqlParameter("@timeType", SqlDbType.NVarChar,2),
					new SqlParameter("@timeSpace", SqlDbType.NVarChar,50),
					new SqlParameter("@everydayFreSpace", SqlDbType.NVarChar,5),
					new SqlParameter("@startTime", SqlDbType.time,3),
					new SqlParameter("@endTime", SqlDbType.time,3),
					new SqlParameter("@startDate", SqlDbType.date,3),
					new SqlParameter("@endDate", SqlDbType.date,3),
					new SqlParameter("@email", SqlDbType.NVarChar,1000),
					new SqlParameter("@article", SqlDbType.NVarChar,200),
					new SqlParameter("@attachmentSource", SqlDbType.NVarChar,100),
					new SqlParameter("@nextDoTimeDate", SqlDbType.DateTime)};
			parameters[0].Value = model.reportSource;
			parameters[1].Value = model.sheetName;
			parameters[2].Value = model.position;
			parameters[3].Value = model.symbol;
			parameters[4].Value = model.aim;
			parameters[5].Value = model.planType;
			parameters[6].Value = model.timeType;
			parameters[7].Value = model.timeSpace;
			parameters[8].Value = model.everydayFreSpace;
			parameters[9].Value = model.startTime;
			parameters[10].Value = model.endTime;
			parameters[11].Value = model.startDate;
			parameters[12].Value = model.endDate;
			parameters[13].Value = model.email;
			parameters[14].Value = model.article;
			parameters[15].Value = model.attachmentSource;
			parameters[16].Value = model.nextDoTimeDate;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(syntec.Model.ExcelRule model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update ExcelRule set ");
			strSql.Append("reportSource=@reportSource,");
			strSql.Append("sheetName=@sheetName,");
			strSql.Append("position=@position,");
			strSql.Append("symbol=@symbol,");
			strSql.Append("aim=@aim,");
			strSql.Append("planType=@planType,");
			strSql.Append("timeType=@timeType,");
			strSql.Append("timeSpace=@timeSpace,");
			strSql.Append("everydayFreSpace=@everydayFreSpace,");
			strSql.Append("startTime=@startTime,");
			strSql.Append("endTime=@endTime,");
			strSql.Append("startDate=@startDate,");
			strSql.Append("endDate=@endDate,");
			strSql.Append("email=@email,");
			strSql.Append("article=@article,");
			strSql.Append("attachmentSource=@attachmentSource,");
			strSql.Append("nextDoTimeDate=@nextDoTimeDate");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@reportSource", SqlDbType.NVarChar,100),
					new SqlParameter("@sheetName", SqlDbType.NVarChar,50),
					new SqlParameter("@position", SqlDbType.VarChar,3),
					new SqlParameter("@symbol", SqlDbType.NVarChar,3),
					new SqlParameter("@aim", SqlDbType.NVarChar,50),
					new SqlParameter("@planType", SqlDbType.NChar,10),
					new SqlParameter("@timeType", SqlDbType.NVarChar,2),
					new SqlParameter("@timeSpace", SqlDbType.NVarChar,50),
					new SqlParameter("@everydayFreSpace", SqlDbType.NVarChar,5),
					new SqlParameter("@startTime", SqlDbType.time,3),
					new SqlParameter("@endTime", SqlDbType.time,3),
					new SqlParameter("@startDate", SqlDbType.date,3),
					new SqlParameter("@endDate", SqlDbType.date,3),
					new SqlParameter("@email", SqlDbType.NVarChar,1000),
					new SqlParameter("@article", SqlDbType.NVarChar,200),
					new SqlParameter("@attachmentSource", SqlDbType.NVarChar,100),
					new SqlParameter("@nextDoTimeDate", SqlDbType.DateTime),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.reportSource;
			parameters[1].Value = model.sheetName;
			parameters[2].Value = model.position;
			parameters[3].Value = model.symbol;
			parameters[4].Value = model.aim;
			parameters[5].Value = model.planType;
			parameters[6].Value = model.timeType;
			parameters[7].Value = model.timeSpace;
			parameters[8].Value = model.everydayFreSpace;
			parameters[9].Value = model.startTime;
			parameters[10].Value = model.endTime;
			parameters[11].Value = model.startDate;
			parameters[12].Value = model.endDate;
			parameters[13].Value = model.email;
			parameters[14].Value = model.article;
			parameters[15].Value = model.attachmentSource;
			parameters[16].Value = model.nextDoTimeDate;
			parameters[17].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ExcelRule ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from ExcelRule ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public syntec.Model.ExcelRule GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,reportSource,sheetName,position,symbol,aim,planType,timeType,timeSpace,everydayFreSpace,startTime,endTime,startDate,endDate,email,article,attachmentSource,nextDoTimeDate from ExcelRule ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			syntec.Model.ExcelRule model=new syntec.Model.ExcelRule();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public syntec.Model.ExcelRule DataRowToModel(DataRow row)
		{
			syntec.Model.ExcelRule model=new syntec.Model.ExcelRule();
			if (row != null)
			{
				if(row["ID"]!=null && row["ID"].ToString()!="")
				{
					model.ID=int.Parse(row["ID"].ToString());
				}
				if(row["reportSource"]!=null)
				{
					model.reportSource=row["reportSource"].ToString();
				}
				if(row["sheetName"]!=null)
				{
					model.sheetName=row["sheetName"].ToString();
				}
				if(row["position"]!=null)
				{
					model.position=row["position"].ToString();
				}
				if(row["symbol"]!=null)
				{
					model.symbol=row["symbol"].ToString();
				}
				if(row["aim"]!=null)
				{
					model.aim=row["aim"].ToString();
				}
				if(row["planType"]!=null)
				{
					model.planType=row["planType"].ToString();
				}
				if(row["timeType"]!=null)
				{
					model.timeType=row["timeType"].ToString();
				}
				if(row["timeSpace"]!=null)
				{
					model.timeSpace=row["timeSpace"].ToString();
				}
				if(row["everydayFreSpace"]!=null)
				{
					model.everydayFreSpace=row["everydayFreSpace"].ToString();
				}
				if(row["startTime"]!=null && row["startTime"].ToString()!="")
				{
					model.startTime=DateTime.Parse(row["startTime"].ToString());
				}
				if(row["endTime"]!=null && row["endTime"].ToString()!="")
				{
					model.endTime=DateTime.Parse(row["endTime"].ToString());
				}
				if(row["startDate"]!=null && row["startDate"].ToString()!="")
				{
					model.startDate=DateTime.Parse(row["startDate"].ToString());
				}
				if(row["endDate"]!=null && row["endDate"].ToString()!="")
				{
					model.endDate=DateTime.Parse(row["endDate"].ToString());
				}
				if(row["email"]!=null)
				{
					model.email=row["email"].ToString();
				}
				if(row["article"]!=null)
				{
					model.article=row["article"].ToString();
				}
				if(row["attachmentSource"]!=null)
				{
					model.attachmentSource=row["attachmentSource"].ToString();
				}
				if(row["nextDoTimeDate"]!=null && row["nextDoTimeDate"].ToString()!="")
				{
					model.nextDoTimeDate=DateTime.Parse(row["nextDoTimeDate"].ToString());
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ID,reportSource,sheetName,position,symbol,aim,planType,timeType,timeSpace,everydayFreSpace,startTime,endTime,startDate,endDate,email,article,attachmentSource,nextDoTimeDate ");
			strSql.Append(" FROM ExcelRule ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" ID,reportSource,sheetName,position,symbol,aim,planType,timeType,timeSpace,everydayFreSpace,startTime,endTime,startDate,endDate,email,article,attachmentSource,nextDoTimeDate ");
			strSql.Append(" FROM ExcelRule ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM ExcelRule ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.ID desc");
			}
			strSql.Append(")AS Row, T.*  from ExcelRule T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "ExcelRule";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

