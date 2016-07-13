using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subgurim.Controles;

namespace LondonTransport
{
    public partial class SearchInRadius : System.Web.UI.Page
    {
        protected string GMap1_Click(object s, GAjaxServerEventArgs e)
        {
            var sb = new StringBuilder();
            GMap gmap = new GMap(e.map);
            gmap.setCenter(e.point, 12);

            sb.Append("subgurim_Points_Clean();");
            gmap.Add("subgurim_Delete();", true);
            gmap.Add($"subgurim_Add({e.point.ToString(e.who)});", true);

            var lst = GetPoints(new string[]
            {
                Convert.ToString(e.point.lat, new CultureInfo("en-US", false)),
                Convert.ToString(e.point.lng, new CultureInfo("en-US",false)),
                numMetres.Text
            });
            
            foreach (var point in lst)
            {
                gmap.Add($"subgurim_AddPoint({point.ToString(gmap.GMap_Id)});",true);
            }
            sb.Append("function subgurim_AddPoint(lat,lng)");
            sb.Append("{");
            //var gIcon = new GIcon()
            //{
            //    flatIconOptions = new FlatIconOptions(12, 12, Color.Green, Color.Black, "C", Color.White, 7,
            //       FlatIconOptions.flatIconShapeEnum.circle)
            //};
            GMarker pointMarker =
                new GMarker("new google.maps.LatLng(lat, lng)")
                {
                    options = new GMarkerOptions(
                        new GIcon(
                            @"http://chart.apis.google.com/chart?cht=it&chs=12x12&chco=008000,000000ff,ffffff01&chl=C&chx=FFFFFF,7&chf=bg,s,00000000&ext=.png"))
                };
            sb.Append(pointMarker.ToString(GMap1.GMap_Id));
            sb.AppendFormat("points.push({0})", pointMarker.ID);
            sb.Append("}");

            sb.Append(gmap);
            var sbStr = sb.ToString();
           
            return sbStr;
        }

        private void RepaintPickers(IEnumerable<GLatLng> lstPoints)
        {
            var mManager = new MarkerManager();

            if (lstPoints == null)
                return;

                foreach (var point in lstPoints)
                {
                    var gIcon = new GIcon()
                    {
                        flatIconOptions = new FlatIconOptions(12, 12, Color.Green, Color.Black, "C", Color.White, 7,
                            FlatIconOptions.flatIconShapeEnum.circle)
                    };

                    var gMarker = new GMarker(point, gIcon);

                    if (GMap1.markerManager == null)
                        GMap1.markerManager = new MarkerManager();

                    mManager.Add(gMarker, 12);
                }

            GMap1.markerManager = mManager;
        }

        private IEnumerable<GLatLng> GetPoints(string[] args)
        {
            var lst = GetDataFromApi.Instance.GetBycyclesInRadius(args);
            var points = lst.Select(p => p.Point);

            return points;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                string eventTarget = Convert.ToString(Request.Params.Get("__EVENTTARGET"));

                var arrStr = Request.Params.Get("__EVENTARGUMENT");

                switch (eventTarget)
                {
                    case "getCoordinate":
                        GMap1.markerManager = null;
                        var eventArgs = Convert.ToString(arrStr).Split(':');
                        RepaintPickers(GetPoints(eventArgs));
                        var lat = Convert.ToDouble(eventArgs[0], new CultureInfo("en-US", false));
                        var lng = Convert.ToDouble(eventArgs[1], new CultureInfo("en-US", false));
                        var latlng = new GLatLng(lat, lng);
                        GMap1.setCenter(latlng);
                        GMap1.Add(new GMarker(latlng));
                        break;
                }
            }
            else
            {
                GMap1.setCenter(new GLatLng(51.5085300, -0.1257400), 12);
                GMap1.mapType = GMapType.GTypes.Normal;
                GMap1.Add(GMapType.GTypes.Physical);
                GMap1.Add(GMapType.GTypes.Hybrid);
                GMap1.Add(new GControl(GControl.preBuilt.MapTypeControl));
                GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));
                GMap1.enableRotation = true;

                StringBuilder sb = new StringBuilder();
                
                sb.Append("var markers = [];");
                sb.Append("function subgurim_Add(point)");
                sb.Append("{");
                GMarker marker = new GMarker(GMap1.GMap_Id + ".getCenter()"); //this is hack, but I can not think of a better
                sb.Append(marker.ToString(GMap1.GMap_Id));
                sb.Append($"markers.push({marker.ID})");
                sb.Append("}");

                sb.Append("function subgurim_Delete()");
                sb.Append("{ if (markers.length > 0) { markers.pop().setMap(null); } }");

                GMap1.Add(sb.ToString());
            }
        }
        
    }
}