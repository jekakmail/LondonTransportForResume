using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Web.Configuration;
using System.Web.UI;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LondonTransport
{
    //Singleton
    public class GetDataFromApi
    {
        private DateTime LastUpdateTime { get; set; }

        private List<CyclePoint> LstCyclePoints { get; set; }

        private static object _sync = new object();

        private static GetDataFromApi _instance = null;
        public static GetDataFromApi Instance
        {
            get
            {
                lock (_sync)
                {
                    if (_instance == null)
                    {
                        _instance = new GetDataFromApi();
                    }
                    return _instance;
                }
            }
        }

        
        protected GetDataFromApi()
        {
            LstCyclePoints = new List<CyclePoint>();
        }

        private void Check()
        {
            if (LstCyclePoints != null && (((DateTime.Now - LastUpdateTime).Minutes > 5) || LstCyclePoints.Count == 0))
            {
                CollectionUpdate();
            }
        }

        private void CollectionUpdate()
        {
            object resultJson = null;

            using (var client = new WebClient())
            {
                dynamic jsonObj =
                    JsonConvert.DeserializeObject(
                        client.DownloadString(
                            $"https://api.tfl.gov.uk/BikePoint?app_id={WebConfigurationManager.AppSettings["application_id"]}&app_key={WebConfigurationManager.AppSettings["application_key"]}"));
                resultJson = jsonObj;
            }

            var enumerable = JArray.FromObject(resultJson);
            if (enumerable != null)
            {
                var newCyclePoints = new List<CyclePoint>();

                foreach (dynamic item in enumerable)
                {
                    var bycylePoint = new CyclePoint(item);

                    newCyclePoints.Add(bycylePoint);
                }

                LstCyclePoints = newCyclePoints;
                LastUpdateTime = DateTime.Now;
            }
        }

        public List<CyclePoint> GetAllCyclePoints()
        {
            Check();

            return LstCyclePoints; 
        }

        public List<CyclePoint> GetAvailiblePoints()
        {
            Check();

            var _lst = LstCyclePoints.Where(point => point.AvailibleBike > 0).ToList();

            return _lst;
        }

        public List<CyclePoint> GetEmptyPoints()
        {
            Check();

            var _lst = LstCyclePoints.Where(point => point.AvailibleBike == 0).ToList();

            return _lst;
        }

        public List<CyclePoint> GetLockedPoints()
        {
            Check();

            var _lst = LstCyclePoints.Where(point => point.Status ).ToList();

            return _lst;
        }
    }
}