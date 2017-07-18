using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.DBUtility;//Please add references
namespace saivian.DAL
{
	/// <summary>
	/// 数据访问类:user_info
	/// </summary>
	public partial class user_info
	{
		public user_info()
		{}
		#region  BasicMethod

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("u_id", "user_info"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int u_id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from user_info");
			strSql.Append(" where u_id=@u_id");
			SqlParameter[] parameters = {
					new SqlParameter("@u_id", SqlDbType.Int,4)
			};
			parameters[0].Value = u_id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(saivian.Model.user_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into user_info(");
			strSql.Append("u_name,s_id,s_name,s_pwd,email,address,s_create_time,zip,phone,city,create_time,create_user,a_level)");
			strSql.Append(" values (");
			strSql.Append("@u_name,@s_id,@s_name,@s_pwd,@email,@address,@s_create_time,@zip,@phone,@city,@create_time,@create_user,@a_level)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@u_name", SqlDbType.VarChar,50),
					new SqlParameter("@s_id", SqlDbType.Int,4),
					new SqlParameter("@s_name", SqlDbType.VarChar,50),
					new SqlParameter("@s_pwd", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,100),
					new SqlParameter("@s_create_time", SqlDbType.VarChar,50),
					new SqlParameter("@zip", SqlDbType.VarChar,10),
					new SqlParameter("@phone", SqlDbType.VarChar,11),
					new SqlParameter("@city", SqlDbType.VarChar,50),
					new SqlParameter("@create_time", SqlDbType.VarChar,50),
					new SqlParameter("@create_user", SqlDbType.VarChar,50),
					new SqlParameter("@a_level", SqlDbType.Int,4)};
			parameters[0].Value = model.u_name;
			parameters[1].Value = model.s_id;
			parameters[2].Value = model.s_name;
			parameters[3].Value = model.s_pwd;
			parameters[4].Value = model.email;
			parameters[5].Value = model.address;
			parameters[6].Value = model.s_create_time;
			parameters[7].Value = model.zip;
			parameters[8].Value = model.phone;
			parameters[9].Value = model.city;
			parameters[10].Value = model.create_time;
			parameters[11].Value = model.create_user;
			parameters[12].Value = model.a_level;

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
		public bool Update(saivian.Model.user_info model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update user_info set ");
			strSql.Append("u_name=@u_name,");
			strSql.Append("s_id=@s_id,");
			strSql.Append("s_name=@s_name,");
			strSql.Append("s_pwd=@s_pwd,");
			strSql.Append("email=@email,");
			strSql.Append("address=@address,");
			strSql.Append("s_create_time=@s_create_time,");
			strSql.Append("zip=@zip,");
			strSql.Append("phone=@phone,");
			strSql.Append("city=@city,");
			strSql.Append("create_time=@create_time,");
			strSql.Append("create_user=@create_user,");
			strSql.Append("a_level=@a_level");
			strSql.Append(" where u_id=@u_id");
			SqlParameter[] parameters = {
					new SqlParameter("@u_name", SqlDbType.VarChar,50),
					new SqlParameter("@s_id", SqlDbType.Int,4),
					new SqlParameter("@s_name", SqlDbType.VarChar,50),
					new SqlParameter("@s_pwd", SqlDbType.VarChar,50),
					new SqlParameter("@email", SqlDbType.VarChar,50),
					new SqlParameter("@address", SqlDbType.VarChar,100),
					new SqlParameter("@s_create_time", SqlDbType.VarChar,50),
					new SqlParameter("@zip", SqlDbType.VarChar,10),
					new SqlParameter("@phone", SqlDbType.VarChar,11),
					new SqlParameter("@city", SqlDbType.VarChar,50),
					new SqlParameter("@create_time", SqlDbType.VarChar,50),
					new SqlParameter("@create_user", SqlDbType.VarChar,50),
					new SqlParameter("@a_level", SqlDbType.Int,4),
					new SqlParameter("@u_id", SqlDbType.Int,4)};
			parameters[0].Value = model.u_name;
			parameters[1].Value = model.s_id;
			parameters[2].Value = model.s_name;
			parameters[3].Value = model.s_pwd;
			parameters[4].Value = model.email;
			parameters[5].Value = model.address;
			parameters[6].Value = model.s_create_time;
			parameters[7].Value = model.zip;
			parameters[8].Value = model.phone;
			parameters[9].Value = model.city;
			parameters[10].Value = model.create_time;
			parameters[11].Value = model.create_user;
			parameters[12].Value = model.a_level;
			parameters[13].Value = model.u_id;

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
		public bool Delete(int u_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from user_info ");
			strSql.Append(" where u_id=@u_id");
			SqlParameter[] parameters = {
					new SqlParameter("@u_id", SqlDbType.Int,4)
			};
			parameters[0].Value = u_id;

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
		public bool DeleteList(string u_idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from user_info ");
			strSql.Append(" where u_id in ("+u_idlist + ")  ");
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
		public saivian.Model.user_info GetModel(int u_id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 u_id,u_name,s_id,s_name,s_pwd,email,address,s_create_time,zip,phone,city,create_time,create_user,a_level from user_info ");
			strSql.Append(" where u_id=@u_id");
			SqlParameter[] parameters = {
					new SqlParameter("@u_id", SqlDbType.Int,4)
			};
			parameters[0].Value = u_id;

			saivian.Model.user_info model=new saivian.Model.user_info();
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
		public saivian.Model.user_info DataRowToModel(DataRow row)
		{
			saivian.Model.user_info model=new saivian.Model.user_info();
			if (row != null)
			{
				if(row["u_id"]!=null && row["u_id"].ToString()!="")
				{
					model.u_id=int.Parse(row["u_id"].ToString());
				}
				if(row["u_name"]!=null)
				{
					model.u_name=row["u_name"].ToString();
				}
				if(row["s_id"]!=null && row["s_id"].ToString()!="")
				{
					model.s_id=int.Parse(row["s_id"].ToString());
				}
				if(row["s_name"]!=null)
				{
					model.s_name=row["s_name"].ToString();
				}
				if(row["s_pwd"]!=null)
				{
					model.s_pwd=row["s_pwd"].ToString();
				}
				if(row["email"]!=null)
				{
					model.email=row["email"].ToString();
				}
				if(row["address"]!=null)
				{
					model.address=row["address"].ToString();
				}
				if(row["s_create_time"]!=null)
				{
					model.s_create_time=row["s_create_time"].ToString();
				}
				if(row["zip"]!=null)
				{
					model.zip=row["zip"].ToString();
				}
				if(row["phone"]!=null)
				{
					model.phone=row["phone"].ToString();
				}
				if(row["city"]!=null)
				{
					model.city=row["city"].ToString();
				}
				if(row["create_time"]!=null)
				{
					model.create_time=row["create_time"].ToString();
				}
				if(row["create_user"]!=null)
				{
					model.create_user=row["create_user"].ToString();
				}
				if(row["a_level"]!=null && row["a_level"].ToString()!="")
				{
					model.a_level=int.Parse(row["a_level"].ToString());
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
			strSql.Append("select u_id,u_name,s_id,s_name,s_pwd,email,address,s_create_time,zip,phone,city,create_time,create_user,a_level ");
			strSql.Append(" FROM user_info ");
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
			strSql.Append(" u_id,u_name,s_id,s_name,s_pwd,email,address,s_create_time,zip,phone,city,create_time,create_user,a_level ");
			strSql.Append(" FROM user_info ");
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
			strSql.Append("select count(1) FROM user_info ");
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
				strSql.Append("order by T.u_id desc");
			}
			strSql.Append(")AS Row, T.*  from user_info T ");
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
			parameters[0].Value = "user_info";
			parameters[1].Value = "u_id";
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

