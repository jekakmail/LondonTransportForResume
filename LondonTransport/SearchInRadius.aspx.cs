using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subgurim.Controles;

namespace LondonTransport
{
    public partial class SearchInRadius : System.Web.UI.Page
    {
        private void BuildMap()
        {
            
        }

        protected string GMap1_ServerEvent(object s, GAjaxServerEventOtherArgs e)
        {
            string js = string.Empty;
            switch (e.eventName)
            {
                case "Click":
                    GLatLng latlng = new GLatLng(
                        Convert.ToDouble(e.eventArgs[2], new System.Globalization.CultureInfo("en-US", false)),
                        Convert.ToDouble(e.eventArgs[3], new System.Globalization.CultureInfo("en-US", false)));

                    //GInfoWindowOptions options = new GInfoWindowOptions
                    //{
                    //    onCloseFn = string.Format(@"
                    //        function()
                    //        {{
                    //            var ev = new serverEvent('InfoWindowClose', {0});
                    //            ev.addArg('My Argument');
                    //            ev.send();
                    //        }}", GMap1.GMap_Id)
                    //};
                    GMarkerOptions opt = new GMarkerOptions()
                    {
                        
                    };

                    GMarker point = new GMarker(latlng,opt);
                    //GInfoWindow window = new GInfoWindow(latlng,
                    //    $"Window Size (px): ({e.eventArgs[0]},{e.eventArgs[1]}). Close Me.", options);

                    //js = window.ToString(e.who);
                    js = point.ToString(e.who);
                    break;
                case "InfoWindowClose":
                    js = $"alert('{e.eventName}: {e.point} - {e.eventArgs[0]} - {DateTime.Now}')";
                    break;
            }
            return js;
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GMap1.Add(new GListener(GMap1.GMap_Id, GListener.Event.click,
                     string.Format(@"
             function(event)
             {{
                 if (!event) return;
                 var ev = new serverEvent('Click', {0});
                 ev.addArg({0}.getDiv().offsetWidth);
                 ev.addArg({0}.getDiv().offsetHeight);
                 ev.addArg(event.latLng.lat());
                 ev.addArg(event.latLng.lng());
                 ev.send();
            }}
            ", GMap1.GMap_Id)));

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