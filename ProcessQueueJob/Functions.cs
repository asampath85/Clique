using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using UserProfilerModel;
using UserProfilerService;
using UserProfilerHelper;

namespace ProcessQueueJob
{
    public class Functions
    {


        public static void ProcessQueueMessage([QueueTrigger("addrequest")] NewRequest twitterInfo)
        {

            RequestService service = new RequestService();
            service.UpdateRequestStatus((int)RequestStatus.Queue, twitterInfo.RequestId);

            ProcessHelper helper = new ProcessHelper();
            var response = helper.ProcessPendingRequests();

            if (response)
                service.UpdateRequestStatus((int)RequestStatus.Processed, twitterInfo.RequestId);
            else
                service.UpdateRequestStatus((int)RequestStatus.Error, twitterInfo.RequestId);

        }
    }
}
