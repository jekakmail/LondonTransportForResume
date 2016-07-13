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
using Tfl.Api.Presentation.Entities;

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

        private readonly string _appId;
        private readonly string _appKey;

        
        protected GetDataFromApi()
        {
            LstCyclePoints = new List<CyclePoint>();
            _appId = WebConfigurationManager.AppSettings["application_id"];
            _appKey = WebConfigurationManager.AppSettings["application_key"];
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
                            $"https://api.tfl.gov.uk/BikePoint?app_id={_appId}&app_key={_appKey}"));
                resultJson = jsonObj;
            }

            var enumerable = JArray.FromObject(resultJson);

            if (enumerable == null) return;

            var newCyclePoints = new List<CyclePoint>();

            foreach (dynamic item in enumerable)
            {
                var bycylePoint = new CyclePoint(item);

                newCyclePoints.Add(bycylePoint);
            }

            LstCyclePoints = newCyclePoints;
            LastUpdateTime = DateTime.Now;
        }

        public List<CyclePoint> GetAllCyclePoints()
        {
            Check();

            return LstCyclePoints; 
        }

        public List<CyclePoint> GetAvailiblePoints()
        {
            Check();

            return LstCyclePoints.Where(point => point.AvailibleBike > 0).ToList();
        }

        public List<CyclePoint> GetEmptyPoints()
        {
            Check();

            return LstCyclePoints.Where(point => point.AvailibleBike == 0).ToList();
        }

        public List<CyclePoint> GetLockedPoints()
        {
            Check();

            return LstCyclePoints.Where(point => point.Status).ToList();
        }

        public List<CyclePoint> GetBycyclesInRadius(string[] args)
        {
            dynamic jsonArrPoints;

            var lstBycycles = new List<CyclePoint>();

            if (args.Length < 2 || args[0] == string.Empty || args[1] == string.Empty || args[2] == string.Empty)
                return lstBycycles;

            using (var client = new WebClient())
            {
                string response = string.Empty;
                try
                {
                    response = client.DownloadString(
                        $"https://api.tfl.gov.uk/BikePoint?lat={args[0]}&lon={args[1]}&radius={args[2]}&app_id={_appId}&app_key={_appKey}");

                }
                catch (WebException ex)
                {
                    (ex.Response as HttpWebResponse).StatusCode.ToString();
                }

                jsonArrPoints = JsonConvert.DeserializeObject(response);
            }
            

            var enumerable = JArray.FromObject(jsonArrPoints["places"]);

            if (enumerable == null) return lstBycycles;
            
            foreach (dynamic item in enumerable)
            {
                var bycylePoint = new CyclePoint(item);

                lstBycycles.Add(bycylePoint);
            }


            return lstBycycles;
        }

    }
}