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
    public partial class getMerchant : System.Web.UI.Page
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
                        case "getMerchantInfo":
                            getMerchantInfo();
                            break;
                        case "getCommodityInfo":
                            getCommodityInfo();
                            break;
                        case"UpMerchant":
                            UpMerchant();
                            break;
                        case "SaveSkList":
                            SaveSkList();
                            break;
                        case "getPosInfo":
                            getPosInfo();
                            break;
                        case "saveUserComList":
                            saveUserComList();
                            break;
                        case "getPrintCommodity":
                            getPrintCommodity();
                            break;
                        case "createRandSkList":
                            createRandSkList();
                            break;
                    }
                }
            }
        }

        private void createRandSkList()
        {
            var total = 0;
            var s_id = Vincent._Request.GetString("s_id", "");
            List<string> sqlList = null;
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            Dictionary<string, string> dis = null;
            while (total < 9010)
            {
                    dis = new Dictionary<string, string>();
                    dis.Add("s_id", s_id);
                    dis.Add("m_id", "(SELECT  top 1  m_id FROM merchant order by newid() )");
                    dis.Add("sk_price", new Random().Next(801, 1988).ToString());
                    dis.Add("sk_date", DateTime.Now.ToString("yyyy-MM-dd") + " " + unitBLL.GetRandByNum(1, 59) + ":" + unitBLL.GetRandByNum(1, 59) + ":" + unitBLL.GetRandByNum(1, 59));
                    dis.Add("create_time", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
                    list.Add(dis);
            }
            sqlList = new List<string>();
            for (var i = 0; i < list.Count; i++)
            {
                sqlList.Add("insert into user_sk_list(s_id,m_id,sk_price,sk_date,create_time) values('" + list[i]["s_id"] + "','" + list[i]["m_id"] + "'," + list[i]["sk_price"] + ",'" + list[i]["sk_date"] + "','" + list[i]["create_time"] + "')");
            }
            var temp = "";
            string errorCode = "";
            int result = common.ImportBySql(sqlList);
            temp = "{\"errorCode\":" + errorCode + ",\"errorMessage\":\"" + errorCode + "\",\"data\":\"" + result + "\"}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }


        private void getPrintCommodity()
        {
            int totalCount = 0;
            int errorCode = 0;
            string errorMessage = "";
            var s_id = Vincent._Request.GetString("s_id");
            var l_id = Vincent._Request.GetString("l_id");
            string where = " where 1 = 1 ";
            if (s_id != "")
                where += " and s_id =" + s_id;
            if (l_id != "")
                where += "  and l_id =" + l_id;
            DataTable dt = null;
            string cloumns = " * ";
            dt = common.SearchListByTableName(" user_xf_list_v ", cloumns, "", where);
            var temp = "";
            temp = "{\"errorCode\":" + errorCode + ",\"errorMessage\":\"" + errorMessage + "\",\"total\":" + totalCount + ",\"data\":" + common.GetJson(dt) + "}";
            if (dt.Rows.Count <= 0) {
                temp = "{\"errorCode\":" + errorCode + ",\"errorMessage\":\"" + errorMessage + "\",\"total\":" + totalCount + ",\"data\":\"" + errorMessage + "\"}";
            }
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void saveUserComList()
        {
           
            string sql = "";
            int result = 0;
            int errorCode = 0;
            string c = Vincent._Request.GetString("c", "");
            DataTable dt = common.JsonToDtb(c);
            string errorMessage = "";
            try
            {
                if (dt.Rows.Count > 0)
                {
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        sql += "insert into user_com_list values('" + dr["s_id"] + "'," + dr["merchant_id"] + "," + dr["commodity_id"] + "," + dr["commodity_price"] + "," + dr["commodity_num"] + ",'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                    }
                    result = common.ExecuteSqlBy(sql);
                }
            }
            catch (Exception e) {
                errorMessage = e.Message;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"errorCode\":\"" + errorMessage + "\",\"data\":" + result + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }
 

        private void getPosInfo()
        {
            int totalCount = 0;
            int errorCode = 0;
            string errorMessage = "";
            var s_id = Vincent._Request.GetString("s_id");
            var l_id = Vincent._Request.GetString("l_id");
            var type= Vincent._Request.GetString("type");
              
            string where = " 1 = 1 ";
            if (s_id != "")
                where += " and s_id =" + s_id;
            if (l_id != "")
                where += "  and l_id =" + l_id;
            if (type != "")
                where += " and is_fx=0 ";
            DataTable dt = null;
            string cloumns = " * ";
            dt = common.SearchList_ByPage(cloumns, where, 1, 10, out totalCount, " l_id ", " user_sk_list_v ");

            //_Json json = new _Json(dt);
            var temp = "";
            temp = "{\"errorCode\":" + errorCode + ",\"errorMessage\":\"" + errorMessage + "\",\"total\":" + totalCount + ",\"data\":" + common.GetJson(dt) + "}";
            if (dt.Rows.Count <= 0) {
                temp = temp = "{\"errorCode\":" + errorCode + ",\"errorMessage\":\"" + errorMessage + "\",\"total\":" + totalCount + ",\"data\":\"" + errorMessage + "\"}";
            }
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void SaveSkList()
        {
            int result = 0;
            int errorCode = 0;
            string errorMessage = "";
            
            var s_id = Vincent._Request.GetString("s_id", "");
            var m_id = Vincent._Request.GetString("m_id", "");
            var sk_price = Vincent._Request.GetFloat("sk_price");
            var sk_date=Vincent._Request.GetString("sk_date","");
            try {
                string sql = "insert into user_sk_list(s_id,m_id,sk_price,sk_date,create_time) values('" + s_id + "'," + m_id + "," + sk_price + ",'" + sk_date + "','" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                result=common.ExecuteSqlBy(sql);
                if (result > 0)
                    result = Convert.ToInt32(common.ExecuteTableReturnNum("user_sk_list"));
            }
            catch (Exception e)
            {
                errorCode = 1;
                errorMessage = e.Message;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"errorCode\":\"" + errorMessage + "\",\"data\":" + result + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void UpMerchant()
        {
            int errorCode = 0;
            int result = 0;
            try
            {
                string m_name = Vincent._Request.GetString("m_name", "");
                string city = Vincent._Request.GetString("m_city", "");
                string address = Vincent._Request.GetString("m_address", "");
                string sql = " insert into merchant values ('" + m_name + "','" + city + "','" + address + "')";
                result = common.ExecuteSqlBy(sql);

            }
            catch (Exception e)
            {
                errorCode = 1;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"data\":" + result + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void getCommodityInfo()
        {
            int errorCode = 0;
            DataTable dt = null;
            try
            {
                string m_name = Vincent._Request.GetString("m_name", "");
                
                string cloumns = " c_id,c_name 商品名称,c_price 单价 ";
                string tableName = "commodity";
                string sort = "";
                string where = "  where 1=1 ";
                if (m_name != "")
                    where += "  and m_name='" + m_name + "'  ";
                 
                dt = common.SearchListByTableName(tableName, cloumns, sort,where);
            }
            catch (Exception e)
            {
                errorCode = 1;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"data\":" + common.GetJson(dt) + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

        private void getMerchantInfo()
        {
            int errorCode = 0;
            DataTable dt = null;
            try
            {
                string cloumns = " m_id,m_name,city,m_address,m_no,zd_no,ck_no,ls_no,cz_no,sd_bank,pc_no,pz_no,sq_no,jy_type ";
                string tableName = "merchant";
                string sort = " city ";
                string where = "  where 1=1 ";
                dt = common.SearchListByTableName(tableName,cloumns, sort,where);
            }
            catch (Exception e)
            {
                errorCode = 1;
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"data\":" + common.GetJson(dt) + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }

    }
}