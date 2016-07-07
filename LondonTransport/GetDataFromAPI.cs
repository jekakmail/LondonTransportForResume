using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Net;
using System.Net.Mime;
using System.Web.Configuration;
using System.Xml;
using Newtonsoft.Json;

namespace LondonTransport
{
    public class GetDataFromApi
    {
       
       

        public GetDataFromApi()
        {
            
        }
        public Object GetCyclePoints()
        {
            Object result =null;

            

            using (var client = new WebClient())
            {
                dynamic res = JsonConvert.DeserializeObject(client.DownloadString($"https://api.tfl.gov.uk/BikePoint?app_id={WebConfigurationManager.AppSettings["application_id"]}&app_key={WebConfigurationManager.AppSettings["application_key"]}"));
                result = res;
            }



            return result;
        }
    }
}