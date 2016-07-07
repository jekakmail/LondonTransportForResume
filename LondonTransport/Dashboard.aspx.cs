using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Subgurim.Controles;

namespace LondonTransport
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            cyclePointsInfo.DataSource = GetDataFromApi.Instance.GetCyclePoints();
            cyclePointsInfo.DataBind();
        }
        
        protected void cyclePointsInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cyclePointsInfo.PageIndex = e.NewPageIndex;
            cyclePointsInfo.DataBind();
        }
    }
}