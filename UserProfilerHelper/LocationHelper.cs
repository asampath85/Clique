using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfilerModel;
using UserProfilerService;

namespace UserProfilerHelper
{
    public class LocationHelper
    {

        public static void ProcessPendingLocation(List<RequestModel> requestList)
        {
            var service = new RequestService();
            foreach (var item in requestList)
            {
                if (!service.IsLocationTweetExist(item))
                {
                    TweetHelper.ExtractLocationTweets(item);

                }
            }
        }
    }
}
