using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Subgurim.Controles;

namespace LondonTransport
{
    public partial class BycyleAvailability : System.Web.UI.Page
    {
        private List<CyclePoint> _cyclePoints { get; set; }
        public string LabelInfo = "Select mode for displaying";
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

        protected void BycycleAvailible_OnClick(object sender, EventArgs e)
        {
            _cyclePoints = GetDataFromApi.Instance.GetAvailiblePoints();
            RepaintPickers();
            if (_cyclePoints != null) LabelInfo =  _cyclePoints.Count > 0 ? $"Found {_cyclePoints.Count} points" : "Not found";
        }

        protected void EmptyDocks_OnClick(object sender, EventArgs e)
        {
            _cyclePoints = GetDataFromApi.Instance.GetEmptyPoints();
            RepaintPickers();
            if (_cyclePoints != null) LabelInfo = _cyclePoints.Count > 0 ? $"Found {_cyclePoints.Count} points" : "Not found";
        }

        protected void LockedPoints_OnClick(object sender, EventArgs e)
        {
            _cyclePoints = GetDataFromApi.Instance.GetLockedPoints();
            RepaintPickers();
            if (_cyclePoints != null) LabelInfo = _cyclePoints.Count > 0 ? $"Found {_cyclePoints.Count} points" : "Not found";
        }

        private void RepaintPickers()
        {
            var mManager = new MarkerManager();

            if (_cyclePoints != null)
            {
                foreach (var cyclePoint in _cyclePoints)
                {
                    var color = cyclePoint.Status ? Color.Red : Color.Blue;

                    var gIcon = new GIcon()
                    {
                        flatIconOptions = new FlatIconOptions(12, 12, color, Color.Black, "C", Color.White, 7,
                            FlatIconOptions.flatIconShapeEnum.circle)
                    };

                    var gPoint = cyclePoint.Point;

                    var gMarker = new GMarker(gPoint, gIcon);

                    var gWindow = new GInfoWindow(gMarker, cyclePoint.CommonName);

                    mManager.Add(gWindow, 12);
                }
            }
            else
            {
                return;
            }
            GMap1.markerManager = mManager;
        }

        protected void ShowAllPoints_OnClick(object sender, EventArgs e)
        {
            _cyclePoints = GetDataFromApi.Instance.GetAllCyclePoints();
            RepaintPickers();
            if (_cyclePoints != null) LabelInfo = _cyclePoints.Count > 0 ? $"Found {_cyclePoints.Count} points" : "Not found";
        }
    }
}