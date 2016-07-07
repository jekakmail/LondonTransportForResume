using System;
using System.Collections;
using Newtonsoft.Json.Linq;
using Subgurim.Controles;
using static System.Boolean;

namespace LondonTransport
{
    public class CyclePoint
    {
        public string Id { get; set; }
        public Uri Uri { get; set; }
        public string CommonName { get; set; }
        public GLatLng Point { get; set; }

        public string TerminalName { get; set; }
        public bool Status { get; set; }
        public int Docks { get; set; }

        public int AvailibleBike { get; set; }
        public int EmptyDocks { get; set; }


        public CyclePoint(dynamic jsonString)
        {
            Id = jsonString["id"].ToString();

            Uri = new Uri(jsonString["url"].ToString());

            CommonName = jsonString["commonName"].ToString();

            Point = new GLatLng(Convert.ToDouble(jsonString["lat"]),Convert.ToDouble(jsonString["lon"]));

            var aditionalProperties = jsonString["additionalProperties"] as IEnumerable;
            if (aditionalProperties != null)
                foreach (dynamic property in aditionalProperties)
                {
                    string key = property["key"].ToString();
                    string value = property["value"].ToString();
                    switch (key)
                    {
                        case "TerminalName":
                            TerminalName = value;
                            break;
                        case "Locked":
                            Status = Parse(value);
                            break;
                        case "NbBikes":
                            AvailibleBike = int.Parse(value);
                            break;
                        case "NbEmptyDocks":
                            EmptyDocks = int.Parse(value);
                            break;
                        case "NbDocks":
                            Docks = int.Parse(value);
                            break;
                    }
                }
        }
    }
}