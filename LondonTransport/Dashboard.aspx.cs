using System;
using System.Collections;
using System.Collections.Generic;
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
        protected void Page_Init(object sender, EventArgs e)
        {
            var response = new GetDataFromApi();
            var jsonObj = response.GetCyclePoints();

            var mManager = new MarkerManager();

            var enumerable = JArray.Parse(jsonObj.ToString());
            if (enumerable != null) 
            {
                foreach (dynamic item in enumerable)
                {
                    var pos = JObject.Parse(item.ToString());
                    var lat = pos["lat"];
                    var lon = pos["lon"];
                      
                    mManager.Add(
                        new GMarker(
                            new GLatLng(
                                Convert.ToDouble(lat), Convert.ToDouble(lon)), 
                                new GIcon() { flatIconOptions = new FlatIconOptions(15, 15, Color.Blue, Color.Black, "C", Color.White, 8,
                                                FlatIconOptions.flatIconShapeEnum.circle)}), 12);
                }
            }
            else
            {
                return;
            }
            GMap1.markerManager = mManager;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GMap1.setCenter(new GLatLng(51.5085300, -0.1257400), 12);
                GMap1.mapType = GMapType.GTypes.Normal;
                GMap1.Add(GMapType.GTypes.Physical);
                GMap1.Add(GMapType.GTypes.Hybrid);
                GMap1.Add(new GControl(GControl.preBuilt.MapTypeControl));
                GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
                GMap1.enableRotation = true;
            }
        }
    }
}