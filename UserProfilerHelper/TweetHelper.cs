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
    public class TweetHelper
    {
        private  const string TwitterConsumerKey = "yeztpKZcCqNBQLEWoondDcvH7";
        private  const string TwitterConsumerSecret = "0kqgCZ1ZzJUHk7VO7XkYwonKVYVxIFX9n5xmgXbBlDHrvdZHVk";
        //private const string TwitterConsumerKey = "unZjcnD6BB0vbU5TfTiXPGnVe";
        //private const string TwitterConsumerSecret = "7VoiPTrbqaq1vnuu87U4CAbYDyfiqJwlSanN6LvzGkfQ43f1fj";

        private static string GetAccessToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, "https://api.twitter.com/oauth2/token ");
            var customerInfo = Convert.ToBase64String(new UTF8Encoding()
                                      .GetBytes(TwitterConsumerKey + ":" + TwitterConsumerSecret));
            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                                                                      "application/x-www-form-urlencoded");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;

            string json = response.Content.ReadAsStringAsync().Result;
            dynamic item = JsonConvert.DeserializeObject<object>(json);
            return item["access_token"];
        }

        /*

        public static void ExtractUserTweets(CliqueClaimRequestModel model)
        {

            string url = "";
            var max_id = "";
            if (string.IsNullOrEmpty(max_id))
                url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}", "100", model.TwitterUserName);
            else
                url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&max_id={2}&since_id={3}",
                        "100", model.TwitterUserName, model.TwitterUserName, model.TwitterUserName);

            var requestUserTimeline = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
            var accessToken = GetAccessToken();
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;

            dynamic jsonResponse = JsonConvert.DeserializeObject<object>(responseUserTimeLine.Content.ReadAsStringAsync().Result);

            var enumerableTweets = (jsonResponse as IEnumerable<dynamic>);

            List<TweetModel> modelList = new List<TweetModel>();
            foreach (var item in enumerableTweets)
            {
                modelList.Add(new TweetModel {
                    Text = item.text.Value,
                    PostedBy = item.user.name.Value,
                    PostedAt = item.created_at.Value,
                    ProfileImageURL = item.user.profile_image_url_https.Value,
                    TweetIdStr = item.id_str.Value,
                    RequestId = model.Id,
                });
               
            }

            var response = SemantriaHelper.AddScore(modelList);
            
            var service = new RequestService();
            service.AddTweetAndUser(response);


        }
         * 
         * */

        private static List<TweetModel> ExtractTweetsByGeoCode(CliqueClaimRequestModel model)
        {
            string url = "";
            var max_id = "";
            if (string.IsNullOrEmpty(max_id))
            {
                //url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q='{0}'%20AND%20{1}", buildingName, city);
                url = string.Format("https://api.twitter.com/1.1/search/tweets.json?geocode={0},{1},10mi&count=100", model.Latitude, model.Longitude);
            }
            else
                url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&max_id={2}&since_id={3}",
                        "100", model.Locality, model.Locality, model.Locality);

            var requestUserTimeline = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
            var accessToken = GetAccessToken();
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;

            dynamic jsonResponse = JsonConvert.DeserializeObject<object>(responseUserTimeLine.Content.ReadAsStringAsync().Result);

            var enumerableTweets = (jsonResponse.statuses as IEnumerable<dynamic>);

            List<TweetModel> modelList = new List<TweetModel>();
            foreach (var item in enumerableTweets)
            {
                modelList.Add(new TweetModel
                {
                    Text = item.text.Value,
                    PostedBy = item.user.name.Value,
                    PostedAt = item.created_at.Value,
                    ProfileImageURL = item.user.profile_image_url_https.Value,
                    TweetIdStr = item.id_str.Value,
                    RequestId = model.Id,
                });

            }

            return modelList;


        }

        private static List<TweetModel> ExtractTweetsByLocationName(CliqueClaimRequestModel model)
        {
            string url = "";
            var max_id = "";
            if (string.IsNullOrEmpty(max_id))
            {
                //url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q='{0}'%20AND%20{1}", buildingName, city);
                url = string.Format("https://api.twitter.com/1.1/search/tweets.json?q='{0}'&count=100", model.Locality);
            }
            else
                url = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?count={0}&screen_name={1}&trim_user=1&max_id={2}&since_id={3}",
                        "100", model.Locality, model.Locality, model.Locality);

            var requestUserTimeline = new HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
            var accessToken = GetAccessToken();
            requestUserTimeline.Headers.Add("Authorization", "Bearer " + accessToken);
            var httpClient = new HttpClient();
            HttpResponseMessage responseUserTimeLine = httpClient.SendAsync(requestUserTimeline).Result;

            dynamic jsonResponse = JsonConvert.DeserializeObject<object>(responseUserTimeLine.Content.ReadAsStringAsync().Result);

            var enumerableTweets = (jsonResponse.statuses as IEnumerable<dynamic>);

            List<TweetModel> modelList = new List<TweetModel>();
            foreach (var item in enumerableTweets)
            {
                modelList.Add(new TweetModel
                {
                    Text = item.text.Value,
                    PostedBy = item.user.name.Value,
                    PostedAt = item.created_at.Value,
                    ProfileImageURL = item.user.profile_image_url_https.Value,
                    TweetIdStr = item.id_str.Value,
                    RequestId = model.Id,
                });

            }

            return modelList;


        }

        public static void ExtractLocationTweets(CliqueClaimRequestModel model)
        {

            var modelList = ExtractTweetsByLocationName(model);
            var response = SemantriaHelper.AddScore(modelList);

            response.AddRange(SemantriaHelper.AddScore(ExtractTweetsByGeoCode(model)));



            var service = new RequestService();
            service.AddTweetAndLocation(modelList);
         

            modelList = SemantriaHelper.AddScore(modelList);
            service.UpdateTweetScore(modelList);



        }
    }
}
