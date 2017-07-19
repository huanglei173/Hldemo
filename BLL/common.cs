using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Maticsoft.DBUtility;
using System.Data.OleDb;
using System.Collections; 
using System.Data.SqlClient;
using System.Web.Script.Serialization;

namespace saivian.BLL
{
    public class common
    {
        /// <summary>
        /// Searches the list_ by page.
        /// </summary>
        /// <param name="strWhere">The STR where.</param>
        /// <param name="start">The start.</param>
        /// <param name="limit">The limit.</param>
        /// <param name="TotalCount">The total count.</param>
        /// <param name="Sort">The sort.</param>
        /// <param name="T">The T.</param>
        /// <returns></returns>
        public static DataTable SearchList_ByPage(string cloumn, string strWhere, int start, int limit, out int TotalCount, string Sort, string T)
        {
            TotalCount = 0;
            StringBuilder sb = new StringBuilder();
            string strSql = "select * from ( " +
                           "   select ROW_NUMBER() OVER (ORDER BY " + Sort + ") AS RowNumber," + cloumn +
                           "   from " + T + "  " +
                           "" + (strWhere.Trim() == "" ? "" : " where " + strWhere) +
                           ") t " +
                           "where t.RowNumber between " + ((start - 1) * limit + 1) + " and " + start * limit + ";";

            strSql += "select count(1) ROW_SUM  from " + T + " " + (strWhere.Trim() == "" ? "" : " where " + strWhere);
            DataSet ds = DbHelperSQL.Query(strSql);
            if (ds.Tables[1].Rows.Count > 0)
                TotalCount = Convert.ToInt32(ds.Tables[1].Rows[0]["ROW_SUM"]);
            return ds.Tables[0];
        }


        //查询单个表
        public static DataTable SearchListByTableName(string tableName, string cloumns, string Sort, string where)
        {
            Sort = Sort != "" ? "  order by " + Sort + " desc " : Sort;
            string sql = "select " + cloumns + " from " + tableName + where + Sort;
            return DbHelperSQL.Query(sql).Tables[0];

        }

        //执行SQL
        public static int ExecuteSqlBy(string sql)
        {
            return DbHelperSQL.ExecuteSql(sql);
        }
        //返回插入的ID
        public static object ExecuteTableReturnNum(string tableName) {
            return DbHelperSQL.GetSingle("select ident_current('" + tableName + "')");
        }
        //执行SQL返回单个数据
        public static object ExecuteSqlReturnNum(string sql)
        {
            return DbHelperSQL.GetSingle(sql);
        }


        public static int GetDataByProc(string procName, SqlParameter[] sp)
        {
            int resultNum = 0;
            DbHelperSQL.RunProcedure(procName, sp, out resultNum);
            return resultNum;
        }

        //执行存储过程返回受影响行数
        public static int getProcCountByDic(string procName, Dictionary<string, string> param)
        {
            List<SqlParameter> ilistStr = new List<SqlParameter>();
            foreach (var item in param)
            {
                ilistStr.Add(new SqlParameter(item.Key, item.Value));
            }
            SqlParameter[] sp = ilistStr.ToArray();
            return common.GetDataByProc(procName, sp);
        }

        public static string GetJson(DataTable dt)
        {
            string returnStr = string.Empty;
            string temp = string.Empty;
            if (dt.Rows.Count > 0)
            {
                returnStr += "[";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    returnStr += "{";
                    foreach (DataColumn dc in dt.Columns)
                    {
                        temp = dt.Rows[i][dc].ToString() + "--" + dc.ColumnName;
                        //this.Response.Write(dr[dc].ToString());
                        returnStr += "\"" + dc.ColumnName + "\":\"" + dt.Rows[i][dc].ToString() + "\",";
                    }
                    returnStr = returnStr.Substring(0, returnStr.Length - 1);
                    returnStr += "}";
                    if (i != dt.Rows.Count - 1)
                    {
                        returnStr += ",";
                    }
                }
                returnStr += "]";
            }
            else
            {
            }
            return returnStr;
        }

        public DataSet ExcelDataSource(string filepath, string sheetname)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filepath + ";Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter oada = new OleDbDataAdapter("select * from [" + sheetname + "$]", strConn);
            DataSet ds = new DataSet();
            oada.Fill(ds);
            return ds;
        }
        public static DataTable ExcelSheetName(string filepath)
        {
            DataTable dt = null;
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + filepath + ";Extended Properties='Excel 12.0; HDR=Yes; IMEX=1'";
            System.Data.OleDb.OleDbConnection Conn = new System.Data.OleDb.OleDbConnection(strConn);
            string strCom = "SELECT * FROM [Sheet1$]";

            try
            {
                Conn.Open();
                System.Data.OleDb.OleDbDataAdapter myCommand = new System.Data.OleDb.OleDbDataAdapter(strCom, Conn);
                DataSet ds = new DataSet();
                myCommand.Fill(ds, "book1");
                Conn.Close();
                dt = ds.Tables[0];
            }
            catch (Exception err)
            {
                return null;
            }
            return dt;
        }

        //执行多少SQL执行事务
        public static int ImportBySql(List<string> strSql)
        {
            return DbHelperSQL.ExecuteSqlTran(strSql);
        }

        public static DataTable JsonToDtb(string json)
        {
            DataTable dataTable = new DataTable();  //实例化

            DataTable result;

            try
            {

                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();

                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值

                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);

                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {

                        if (dictionary.Keys.Count == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                string value = dictionary[current].ToString();
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                        dataTable.AcceptChanges();
                    }
                }
            }
            catch
            {
            }
            result = dataTable;
            return result;
        }
    }
}
