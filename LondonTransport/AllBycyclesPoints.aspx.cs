using System;
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
    public partial class WebForm1 : System.Web.UI.Page
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
                    var el = JObject.Parse(item.ToString());
                    var lat = el["lat"];
                    var lon = el["lon"];
                    var commonName = el["commonName"].ToString();

                    var gIcon = new GIcon()
                    {
                        flatIconOptions = new FlatIconOptions(15, 15, Color.Blue, Color.Black, "C", Color.White, 8,
                            FlatIconOptions.flatIconShapeEnum.circle)
                    };

                    var gPoint = new GLatLng(Convert.ToDouble(lat), Convert.ToDouble(lon));

                    var gMarker = new GMarker(gPoint, gIcon);

                    var gWindow = new GInfoWindow(gMarker, commonName);

                    mManager.Add(gWindow, 12);
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