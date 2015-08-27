using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserProfilerModel;
using UserProfilerService;

namespace UserProfilerHelper
{
    public class UserHelper    
    {
     


        public static void ProcessPendingUsers(List<RequestModel> requestList)
        {
            var service = new RequestService();
            foreach (var item in requestList)
            {
                if (!service.IsUserTweetExist(item))
                {
                    TweetHelper.ExtractUserTweets(item);
                   
                }
            }
        }

    


    }
}
