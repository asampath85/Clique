using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using UserProfilerWeb.Models;
using Point = DotNet.Highcharts.Options.Point;

namespace UserProfilerWeb.Controllers
{
    public class TimeLineController : Controller
    {
        // GET: TimeLine
        public ActionResult Index()
        {
            var highchartSample = new List<TimeLineModel>
            {
                new TimeLineModel() {Parameters = "Event", GoodScore = 23.45D, AverageScore = 15.32D,BadScore = 9.4D,ActualScore=78.33D},
                new TimeLineModel() {Parameters = "Weather",GoodScore=45.67D,AverageScore = 33.24D,BadScore = 12.23D,ActualScore = 56.22D},
                new TimeLineModel() {Parameters = "User Review",GoodScore=67.23D,AverageScore = 31.23D,BadScore = 10.11D,ActualScore = 29.44D},
                new TimeLineModel() {Parameters = "Tweets",GoodScore = 89.67D,AverageScore = 12.33D,BadScore = 3.43D,ActualScore = 88.11D},
                new TimeLineModel() {Parameters = "Persona",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D,ActualScore = 35.08D},
                new TimeLineModel() {Parameters = "Crime",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D,ActualScore = 24.87D}
            };

            var xDataParameters = highchartSample.Select(i => i.Parameters).ToArray();
            var actualScore = highchartSample.Select(i => i.ActualScore);

            var chart = new Highcharts("chart");
            chart.InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar });
            chart.SetTitle(new Title { Text = "Risk Score Profiling" });
            chart.SetSubtitle(new Subtitle { Text = "Risk predicting using social media" });
            chart.SetXAxis(new XAxis { Categories = xDataParameters });
            chart.SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Scores" }, Max = 100 });
            chart.SetLegend(new Legend { Enabled = false, });
            chart.SetTooltip(new Tooltip
            {
                Enabled = true,
                Formatter = @"function(){return '<b>' + this.series.name +'</b><br/>' + this.x+':' + this.y;}"
            });
            chart.SetPlotOptions(new PlotOptions
            {
                //Series = new PlotOptionsSeries() { Stacking = Stackings.Normal },
                Bar = new PlotOptionsBar
                {
                    DataLabels = new PlotOptionsBarDataLabels { Enabled = true, Color = Color.Maroon, Shadow = true },
                    //PointWidth = 10,
                    //GroupPadding = 1,
                    //PointPadding = 0,
                    Shadow = true,
                    BorderWidth = 1,
                    BorderColor = Color.FloralWhite,
                }
            });
            Data data = new Data(
                actualScore.Select(y => new Point { Color = GetBarColor(y), Y = y }).ToArray()
            );

            chart.SetSeries(new Series { Name = "Actual Score", Data = data });

            return View(chart);
        }

        private Color GetBarColor(double value)
        {
            if (value > 0 && value <= 30) return Color.Red;
            if (value > 30 && value <= 60) return Color.Yellow;
            return Color.ForestGreen;
        }
    }
}