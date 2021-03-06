﻿using System;
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
        public static string CountPoints { get; set; }
        public static string AvailibleBycycles { get; set; }

        public static string EmptyDocks { get; set; }

        public static string Docks { get; set; }
        private List<CyclePoint> _resData;
        protected void Page_Init(object sender, EventArgs e)
        {
            _resData = GetDataFromApi.Instance.GetAllCyclePoints();

            CountPoints = _resData.Count.ToString();

            AvailibleBycycles = (from cyclePoint in _resData
                                 select cyclePoint.AvailibleBike).Sum().ToString();

            EmptyDocks = (from cyclePoint in _resData
                select cyclePoint.EmptyDocks).Sum().ToString();

            Docks = (from cyclePoint in _resData
                          select cyclePoint.Docks).Sum().ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            cyclePointsInfo.DataSource = _resData;
            cyclePointsInfo.DataBind();
        }
        
        protected void cyclePointsInfo_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            cyclePointsInfo.PageIndex = e.NewPageIndex;
            cyclePointsInfo.DataBind();
        }
    }
}