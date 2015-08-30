using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserProfilerModel;
using UserProfilerService;

namespace UserProfilerHelper
{
    public class EventHelper
    {
        const string key = "qRg2vNsFsqChXjnK";
        public static void ProcessPendingEvents(IList<CliqueClaimRequestModel> requestList)
        {
            var service = new RequestService();
            foreach (var item in requestList)
            {
                if (!service.IsEventExist(item))
                {
                    var eventList = ExtractEventsFromAPI(item.City);
                    service.AddEvent(MapEventModel(eventList, item));
                }
               
            }
           

        }

        public static List<EventModel> MapEventModel(List<Event> eventList, CliqueClaimRequestModel requestModel)
        {
            List<EventModel> response = new List<EventModel>();
            foreach (var item in eventList)
            {
                response.Add(new EventModel
                {
                    EventId = item.eventId,  
                    RequestId = requestModel.Id,
                    Name = item.title,
                    Description = item.description,
                    StartDate = item.start_time ?? DateTime.Now,
                    EndDate = item.stop_time??DateTime.Now,
                    Venue = item.venue_name
                });

            }

            return response;
        }

        public static List<Event> ExtractEventsFromAPI(string location)
        {
            List<Event> returnObject = null;

            try
            {
                string categories = AppendCategories();

                string url = string.Format("http://api.eventful.com/json/events/search?app_key={0}&{2}&l={1}&within=10&units=miles&page_size=100", key, location, categories);
               // string url = string.Format("http://api.eventful.com/json/events/search?app_key={0}&l={1}&within=10&units=miles&page_size=100", key, location);

                var requestUserTimeline = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);

                var httpClient = new HttpClient();
                HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;
                var response = responseUserTimeLine.Content.ReadAsStringAsync().Result;
                dynamic wrapper = JsonConvert.DeserializeObject<object>(response);
                if (wrapper.events == null || wrapper.page_count == 0)
                    return new List<Event>();

                List<dynamic> eventList = new List<dynamic>();
               
                if(wrapper.page_count == 1)
                {
                    eventList.Add(wrapper.events.@event as dynamic);
                }
                else
                    eventList = (wrapper.events.@event as List<dynamic>).ToList();


                for (int i = 2; i <= Convert.ToInt32(wrapper.page_count); i++)
                {
                    url = string.Format("http://api.eventful.com/json/events/search?app_key={0}&{2}&l={1}&within=10&units=miles&page_number={3}&page_size=100", key, location, categories, i);
                    var requestUserTimeline1 = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                    var httpClient1 = new HttpClient();
                    HttpResponseMessage responseUserTimeLine1 = httpClient1.SendAsync(requestUserTimeline1).Result;
                    response = responseUserTimeLine1.Content.ReadAsStringAsync().Result;
                    dynamic pageWrapper = JsonConvert.DeserializeObject<object>(response);
                    if (pageWrapper.events == null || wrapper.page_count == 0)
                        continue;

                    if (pageWrapper.page_count == 1)
                    {
                        eventList.Add(pageWrapper.events.@event as dynamic);
                    }
                    else
                        eventList.AddRange((pageWrapper.events.@event as List<dynamic>).ToList());                  

                }

                returnObject = new List<Event>();
                foreach (var item in eventList)
                {
                    returnObject.Add(
                        new Event {
                            eventId = item.id,
                            city_name = item.city_name.Value,
                            description = item.description.Value,
                            start_time = DateTime.Parse(item.start_time.Value),
                            stop_time = DateTime.Parse(item.stop_time.Value??item.start_time.Value),
                            title = item.title.Value,
                            venue_name = item.venue_name.Value
                        }
                        );

                }
            }
            catch(Exception e)
            {

            }

            return returnObject;
        }

        static string AppendCategories()
        {
            List<string> Categories = new List<string> { "sports" };
            StringBuilder catFilter = new StringBuilder(1000);
            //catFilter.Append("(");
            var lastCat = Categories.Last();
            foreach (var item in Categories)
            {
                if (item != lastCat)
                {
                    catFilter.Append("q=" + item + "||");
                }
                else
                {
                    catFilter.Append("q=" + item);
                }

            }
            return catFilter.ToString();
        }
    }



    public class Event
    {
        public DateTime? start_time { get; set; }
        public DateTime? stop_time { get; set; }
        public string venue_name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string city_name { get; set; }
        public string eventId { get; set; }

    }
}
