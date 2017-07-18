using saivian.BLL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saivian.Web.ajax
{
    public partial class getFiles : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                HttpPostedFile file = Request.Files[0];
                int errorCode=0;
                string path = "/files/";
                DataTable dt = null;
                string uploadPath = HttpContext.Current.Server.MapPath(path);
                if (file != null)
                {
                    try
                    {
                        if (!Directory.Exists(uploadPath))
                        {
                            Directory.CreateDirectory(uploadPath);
                        }
                        file.SaveAs(uploadPath + file.FileName);
                        var lastlength = file.FileName.LastIndexOf('.');
                        var lastName = file.FileName.Substring(lastlength + 1, file.FileName.Length - lastlength - 1);
                       
                        if (lastName == "xlsx")
                        {
                            dt = common.ExcelSheetName(uploadPath + file.FileName);
                        }
                    }
                    catch(Exception){
                        errorCode=1;
                    }
                    string temp = "{\"errorCode\":" + errorCode + ",\"data\":" + common.GetJson(dt) + "}";
                    HttpContext.Current.Response.Write(temp);
                    HttpContext.Current.Response.End();
                }
                else
                {
                    HttpContext.Current.Response.Write("0");
                    HttpContext.Current.Response.End();
                }
            }
        }
    }
}