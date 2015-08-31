using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class WeatherModel
    {

        public int Id { get; set; }
        public int RequestId { get; set; }
        public string MinTemp { get; set; }
        public string MaxTemp { get; set; }
        public string WindSpeed { get; set; }
        public string Humidity { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public string Temp { get; set; }
        public string WeatherDay { get; set; }
        public string WindDirection { get; set; }
        public string Weather { get; set; }

    }
}
