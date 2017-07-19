using saivian.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saivian.Web.ajax
{
    public partial class UpDataByFiles : System.Web.UI.Page
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
                        case "SaveUserByFile":
                            SaveUserByFile();
                            break;
                    }
                }
            }
        }

        private void SaveUserByFile()
        {
            int errorCode = 0;
            string errorMessage = "";
            int result = 0;
            try
            {
                var fileName = Vincent._Request.GetString("fileName", "");
                var fileType = Vincent._Request.GetString("fileType", "");
                var current_uname = "admin";
                string path = "/files/";
                DataTable dt = null;
                string uploadPath = HttpContext.Current.Server.MapPath(path);
                dt = common.ExcelSheetName(uploadPath + fileName);
                List<string> strSql = new List<string>();
                
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    switch(fileType){
                        case "user_up":
                            strSql.Add("INSERT INTO user_info (u_name,s_id,s_name,s_pwd,email,email_pwd,address,s_create_time,s_bk_time,zip ,phone,city,extend_num,fx_num ,create_time,create_user,last_s_id)VALUES ('" + dr["姓名"] + "','" + dr["saivianID"] + "','" + dr["saivian账号"] + "','" + dr["saivian密码"] + "','" + dr["邮箱"] + "','" + dr["邮箱密码"] + "','" + dr["地址"] + "','" + dr["创建日期"] + "','" + dr["绑卡日期"] + "','" + dr["邮编"] + "','" + dr["电话"] + "','" + dr["城市"] + "',1,0,'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "','" + current_uname + "','" + dr["推荐人"] + "');");
                            strSql.Add("INSERT INTO employee (s_id,bank_no,bank_name,card_date,card_type) values('" + dr["saivianID"] + "','" + dr["银行卡号"] + "','" + dr["银行名称"] + "','" + dr["卡片有效期"] + "','" + dr["卡片种类"] + "')");
                            break;
                        case "merchant_up":
                            strSql.Add("INSERT INTO merchant (m_name ,city  ,m_address ,m_no ,zd_no ,ck_no ,ls_no,cz_no  ,sd_bank ,pc_no ,pz_no ,sq_no ,jy_type) values('" + dr["商户名称"] + "','" + dr["商户城市"] + "','" + dr["商户地址"] + "','" + dr["商户编号"] + "','" + dr["终端号"] + "','" + dr["参考号"] + "','" + dr["流水号"] + "','" + dr["操作员"] + "','" + dr["收单行"] + "','" + dr["批次号"] + "','" + dr["凭证号"] + "','" + dr["授权号"] + "','" + dr["交易类型"] + "');");
                            break;
                        case "commodity_up":
                            strSql.Add("INSERT INTO commodity (m_name,c_name,c_price) values('" + dr["商户名称"] + "','" + dr["商品名称"] + "'," + dr["商品单价"] + ");");
                            break;
                   } 
                }
                result = common.ImportBySql(strSql);
            }
            catch (Exception e)
            {
                errorCode = 1;
                errorMessage =  e.Message.Trim();
            }
            string temp = "{\"errorCode\":" + errorCode + ",\"errorMessage\":\" " + errorMessage + " \",\"data\":" + result + "}";
            HttpContext.Current.Response.Write(temp);
            HttpContext.Current.Response.End();
        }
    }
}