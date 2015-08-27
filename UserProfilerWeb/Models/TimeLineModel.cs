using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfilerWeb.Models
{
    public class TimeLineModel
    {
        public string Month { get; set; }

        public string Parameters { get; set; }

        public double GoodScore { get; set; }

        public double AverageScore { get; set; }

        public double BadScore { get; set; }

        public double ActualScore { get; set; }
    }
}