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
using System;

namespace UserProfilerWeb.Controllers
{
    public class TimeLineController : Controller
    {
        RequestAPIController apiController;
        
        //// GET: TimeLine
        //public ActionResult OldIndex()
        //{
        //    var highchartSample = new List<TimeLineModel>
        //    {
        //        new TimeLineModel() {Parameters = "Event", GoodScore = 23.45D, AverageScore = 15.32D,BadScore = 9.4D,ActualScore=78.33D},
        //        new TimeLineModel() {Parameters = "Weather",GoodScore=45.67D,AverageScore = 33.24D,BadScore = 12.23D,ActualScore = 56.22D},
        //        new TimeLineModel() {Parameters = "User Review",GoodScore=67.23D,AverageScore = 31.23D,BadScore = 10.11D,ActualScore = 29.44D},
        //        new TimeLineModel() {Parameters = "Tweets",GoodScore = 89.67D,AverageScore = 12.33D,BadScore = 3.43D,ActualScore = 88.11D},
        //        new TimeLineModel() {Parameters = "Persona",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D,ActualScore = 35.08D},
        //        new TimeLineModel() {Parameters = "Crime",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D,ActualScore = 24.87D}
        //    };

        //    var xDataParameters = highchartSample.Select(i => i.Parameters).ToArray();
        //    var actualScore = highchartSample.Select(i => i.ActualScore);

        //    var chart = new Highcharts("chart");
        //    chart.InitChart(new Chart { DefaultSeriesType = ChartTypes.Bar });
        //    chart.SetTitle(new Title { Text = "Risk Score Profiling" });
        //    chart.SetSubtitle(new Subtitle { Text = "Risk predicting using social media" });
        //    chart.SetXAxis(new XAxis { Categories = xDataParameters });
        //    chart.SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Scores" }, Max = 100 });
        //    chart.SetLegend(new Legend { Enabled = false, });
        //    chart.SetTooltip(new Tooltip
        //    {
        //        Enabled = true,
        //        Formatter = @"function(){return '<b>' + this.series.name +'</b><br/>' + this.x+':' + this.y;}"
        //    });
        //    chart.SetPlotOptions(new PlotOptions
        //    {
        //        //Series = new PlotOptionsSeries() { Stacking = Stackings.Normal },
        //        Bar = new PlotOptionsBar
        //        {
        //            DataLabels = new PlotOptionsBarDataLabels { Enabled = true, Color = Color.Maroon, Shadow = true },
        //            //PointWidth = 10,
        //            //GroupPadding = 1,
        //            //PointPadding = 0,
        //            Shadow = true,
        //            BorderWidth = 1,
        //            BorderColor = Color.FloralWhite,
        //        }
        //    });
        //    Data data = new Data(
        //        actualScore.Select(y => new Point { Color = GetBarColor(y), Y = y }).ToArray()
        //    );

        //    chart.SetSeries(new Series { Name = "Actual Score", Data = data });

        //    return View(chart);
        //}

        //public ActionResult Index1()
        //{

        //    apiController = new RequestAPIController();

        //    GetEventScore(57);

        //    var highchartSample = new List<TimeLineModel>
        //    {
        //        new TimeLineModel() {Parameters = "Event", GoodScore = 23.45D, AverageScore = 15.32D,BadScore = 9.4D,ActualScore=78.33D},
        //        new TimeLineModel() {Parameters = "Weather",GoodScore=45.67D,AverageScore = 33.24D,BadScore = 12.23D,ActualScore = 56.22D},
        //        new TimeLineModel() {Parameters = "User Review",GoodScore=67.23D,AverageScore = 31.23D,BadScore = 10.11D,ActualScore = 29.44D},
        //        new TimeLineModel() {Parameters = "Tweets",GoodScore = 89.67D,AverageScore = 12.33D,BadScore = 3.43D,ActualScore = 88.11D},
        //        new TimeLineModel() {Parameters = "Persona",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D,ActualScore = 35.08D},
        //        new TimeLineModel() {Parameters = "Crime",GoodScore=38.34D,AverageScore = 25.34D,BadScore = 16.43D,ActualScore = 24.87D}
        //    };

        //    var xDataParameters = highchartSample.Select(i => i.Parameters).ToArray();
        //    var actualScore = highchartSample.Select(i => i.ActualScore);

        //    Highcharts chart = new Highcharts("chart")
        //        .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
        //        .SetTitle(new Title { Text = "Risk Score Profiling" })
        //        .SetSubtitle(new Subtitle { Text = "Risk predicting using social media" })
        //        .SetXAxis(new XAxis { Categories = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" } })
        //        .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Scores" }, Max = 100 })
        //        .SetLegend(new Legend
        //        {
        //            Layout = Layouts.Vertical,
        //            Align = HorizontalAligns.Left,
        //            VerticalAlign = VerticalAligns.Top,
        //            X = 100,
        //            Y = 70,
        //            Floating = true,
        //            BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
        //            Shadow = true
        //        })
        //        .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' mm'; }" })
        //        .SetPlotOptions(new PlotOptions
        //        {
        //            Column = new PlotOptionsColumn
        //            {
        //                PointPadding = 0.2,
        //                //BorderWidth = 0,
        //                Shadow = true,
        //                BorderWidth = 1,
        //                BorderColor = Color.FloralWhite
        //            }
        //        })
                
        //        .SetSeries(new[]
        //        {
        //            new Series { Name = "Event", Data = new Data(new object[] { 49.9, 21.5, 46.4, 49.2, 44.0, 76.0, 35.6, 48.5, 16.4, 44.1, 55.6, 54.4 }) },
        //            new Series { Name = "Weather", Data = new Data(new object[] { 8.9, 8.8, 9.3, 21.4, 27.0, 28.3, 29.0, 29.6, 22.4, 25.2, 29.3, 31.2 }) },
        //            new Series { Name = "User Review", Data = new Data(new object[] { 33.6, 48.8, 98.5, 93.4, 36.0, 84.5, 35.0, 34.3, 31.2, 83.5, 16.6, 92.3 }) },
        //            new Series { Name = "Persona", Data = new Data(new object[] { 42.4, 33.2, 34.5, 39.7, 52.6, 75.5, 57.4, 60.4, 47.6, 39.1, 46.8, 51.1 }) },
        //            new Series { Name = "Crime", Data = new Data(new object[] { 22.4, 23.2, 44.5, 59.7, 72.6, 45.5, 37.4, 20.4, 47.6, 19.1, 26.8, 31.1 }) }
        //        });

        //    return View(chart);
        //}

        public ActionResult Index(int id)
        {
            ViewBag.RequestId = id;
          
            apiController = new RequestAPIController();

            Highcharts chart12 = new Highcharts("chart")
               .InitChart(new Chart
               {
                   Type = ChartTypes.Column,
                   Margin = new[] { 75 },
                   Options3d = new ChartOptions3d
                   {
                       Enabled = true,
                       Alpha = 22,
                       Beta = 4,
                       Depth = 50,
                       ViewDistance = 25
                   }
               })
               .SetXAxis(new XAxis { Categories = GetMonthList() })
               .SetTitle(new Title { Text = "Event Timeline" })               
               .SetPlotOptions(new PlotOptions { Column = new PlotOptionsColumn { Depth = 25,Color = Color.LightSlateGray,BorderColor = Color.AntiqueWhite,BorderWidth = 1} })
               .SetLegend(new Legend { Enabled = false })

               .SetSeries(new Series { Data = new Data(GetEventScore(id)) });

            TimeLineModel model = new TimeLineModel { chart = chart12, TweetScore = GetTweetScore(id), UserScore = GetUserFeedbackScore(id), WeatherScore = 60, CrimeScore = 20, LocationAura = (GetUserFeedbackScore(id) + 60 + 20)/3};

            return View(model);
        }

        private Color GetBarColor(double value)
        {
            if (value > 0 && value <= 30) return Color.Red;
            if (value > 30 && value <= 60) return Color.Yellow;
            return Color.ForestGreen;
        }

        private double GetUserFeedbackScore(int id)
        {
            var userFeedbacks = apiController.GetUserFeedback(id);

            var totalFeedbacks = userFeedbacks.Count;
            var totalPositiveFeedbacks = userFeedbacks.Count(x => x.Score > 0.3);

            double score = totalFeedbacks > 0 ? Math.Round(((double)totalPositiveFeedbacks / (double)totalFeedbacks) * 100,2) : 0;
            return score;

        }

        private double GetTweetScore(int id)
        {
            var tweets = apiController.GetLocationTweet(id);

            var totalTweets = tweets.Count;
            var totalPositiveTweets = tweets.Count(x => x.Score > 0.3);

            double tweetScore = totalTweets>0 ? Math.Round(((double)totalPositiveTweets /(double) totalTweets) * 100,2):0;
            return tweetScore;

        }

        private object[] GetEventScore(int id)
        {
            var events = apiController.GetEvent(id);

            var groupedByMonth = events.GroupBy(x => x.StartDate.ToString("MMM"));

            List<string> monthList = new List<string> { "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" };

            List<object> eventScoreList = new List<object>();
            var totalEvents = events.Count();
            foreach (var item in monthList)
            {
                var eventsInthisMonth = groupedByMonth.Where(x => x.Key.Trim().Equals(item)).Select(x => x.Count()).FirstOrDefault();


                var eventScore = ((double)eventsInthisMonth / (double)totalEvents) * 100;
                eventScoreList.Add(eventScore);
            }

            return eventScoreList.ToArray();
        }

        private string[] GetMonthList()
        {
            List<string> monthList = new List<string> { "Aug", "Sep", "Oct", "Nov", "Dec", "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul" };
            return monthList.ToArray();
        }



    }
}