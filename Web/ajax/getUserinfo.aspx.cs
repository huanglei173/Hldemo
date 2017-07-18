using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using saivian.BLL;
using System.Data;
using Vincent;
 

namespace saivian.Web.ajax
{
    public partial class getUserinfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string action = Vincent._Request.GetString("Action", "");
                if (action != null && action != "")
                {
                    switch (action)
                    {
                        case "getUserInfoByParams":
                            getUserInfoByParams();
                            break;
                        case "getUserManagerByParams":
                            getUserManagerByParams();
                            break;
                        case "up_UserXF":
                            up_UserXF();
                            break;
                        case "SaveUserDate":
                            SaveUserDate();
                            break;
                    }
                }
            }
        }

        private void SaveUserDate()
        {
            string sql = "";
            int result = 0;
            int errorCode = 0;
            string s_id = Vincent._Request.GetString("s_id", "");
            string t_name = Vincent._Request.GetString("t_name", "");
            string t_date = Vincent._Request.GetString("t_date", "");
            string [] sidList  = s_id.Split(';');

            string errorMessage = "";
            try
            {
                for (var i = 0; i < sidList.Length-1; i++)
                {
                    sql += " insert into user_date (s_id,t_name,t_date,up_date) values('" + sidList[i] + "','" + t_name + "','" + t_date + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "');";
                }
                result = common.ExecuteSqlBy(sql);
            }
            catch (Exception e)
            {
                errorMessage = e.Message;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"errorCode\":\"" + errorMessage + "\",\"data\":" + result + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }
 

        private void up_UserXF()
        {
            int errorCode = 0;
            int result = 0;
            try
            {
                string procName = "proc_xf";
                string s_id = Vincent._Request.GetString("s_id","");
                string[] sidList = s_id.Split(';');
                string create_user = "1";
                for (var i = 0; i < sidList.Length-1; i++)
                {
                    Dictionary<string, string> str = new Dictionary<string, string>();
                    str.Add("s_id", sidList[i]);
                    str.Add("current_user", create_user);
                    result += common.getProcCountByDic(procName, str);
                }
            }
            catch (Exception e)
            {
                errorCode = 1;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"data\":" + result + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void getUserManagerByParams()
        {
            int totalCount = 0;
            int errorCode = 0;
            string errorMessage = "";
            DataTable dt = null;
            string cloumns = "  *   ";
            try
            {
                dt = common.SearchList_ByPage(cloumns, "", 1, 10, out totalCount, "u_id", " user_manager ");
            }
            catch (Exception e)
            {
                errorCode = 1;
                errorMessage = e.Message.Trim();
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"errorCode\":\"" + errorMessage + "\",\"total\":" + totalCount + ",\"data\":" + common.GetJson(dt) + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void getUserInfoByParams()
        {
            int totalCount = 0;
            int errorCode = 0;
            string errorMessage = "";
            DataTable dt=null;
            //string cloumns = "  u_id,u_name,s_id,s_name,s_pwd,email,address,s_create_time,zip,phone,city  ";
            string cloumns = " * ";
            try
            {
                dt = common.SearchList_ByPage(cloumns, "", 1, 10, out totalCount, "u_id", " user_manager ");
            }
            catch (Exception e)
            {
                errorCode = 1;
                errorMessage = e.Message.Trim();
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"errorCode\":\" " + errorMessage + "\",\"total\":" + totalCount + ",\"data\":" + common.GetJson(dt) + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }
    }
}