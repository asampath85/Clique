using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UserProfilerModel;
using UserProfilerService;

namespace UserProfilerHelper
{
    public class WeatherHelper
    {
        private const string OpenWeatherAPIKey = "3128cb5e2287c9b5c3e2c3eaa18db4ee";

        public static bool ProcessLocation(IList<CliqueClaimRequestModel> requestList)
        {
            var service = new RequestService();
            foreach (var item in requestList)
            {
                if (!service.IsLocationWeatherExist(item))
                {
                    IList<WeatherModel> result = ExtractLocationWeather(item);

                    service.AddLocationWeather(result, item.Id);
                }
            }

            return true;
        }

        private static IList<WeatherModel> ExtractLocationWeather(CliqueClaimRequestModel item)
        {
            string url = string.Format
              ("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&type=accurate&mode=xml&units=metric&cnt=7appid={1}",
              item.City, OpenWeatherAPIKey);

            using (WebClient client = new WebClient())
            {
                try
                {
                    string response = client.DownloadString(url);
                    if (!(response.Contains("message") && response.Contains("cod")))
                    {
                        XElement xEl = XElement.Load(new System.IO.StringReader(response));
                       return  GetWeatherInfo(xEl);
                    }

                }

                catch (Exception es)
                {

                }
            }

            return new List<WeatherModel>();


        }

        private static IList<WeatherModel> GetWeatherInfo(XElement xEl)
        {
            IEnumerable<WeatherModel> w = xEl.Descendants("time").Select((el) =>
                new WeatherModel
                {
                    Humidity = el.Element("humidity").Attribute("value").Value + "%",
                    MaxTemp = el.Element("temperature").Attribute("max").Value + "°",
                    MinTemp = el.Element("temperature").Attribute("min").Value + "°",
                    Temp = el.Element("temperature").Attribute("day").Value + "°",
                    Weather = el.Element("symbol").Attribute("name").Value,
                    WeatherDay = DayOfTheWeek(el),                    
                    WindDirection = el.Element("windDirection").Attribute("name").Value,
                    WindSpeed = el.Element("windSpeed").Attribute("mps").Value + "mps"
                });

        

            return w.ToList();
        }

        private static string DayOfTheWeek(XElement el)
        {
            DateTime dW = Convert.ToDateTime(el.Attribute("day").Value);
            return dW.ToShortDateString();
        }

    }
}
