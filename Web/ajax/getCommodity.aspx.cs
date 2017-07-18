using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace saivian.Web.ajax
{
    public partial class getCommodity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack) { 
                string action = Vincent._Request.GetString("Action", "");
                if (action != null && action != "")
                {
                    switch (action) { 
                        case "getCommodityInfoBySid":
                            getCommodityInfoBySid();
                            break;
                    }
                }
            }
        }

        private void getCommodityInfoBySid()
        {
             
        }
    }
}