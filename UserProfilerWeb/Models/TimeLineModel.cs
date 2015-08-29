using DotNet.Highcharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfilerWeb.Models
{
    public class TimeLineModel
    {
        public string Month { get; set; }

        public double UserScore { get; set; }

        public double CrimeScore { get; set; }

        public double WeatherScore { get; set; }

        public double TweetScore { get; set; }

        public Highcharts chart { get; set; }
    }
}