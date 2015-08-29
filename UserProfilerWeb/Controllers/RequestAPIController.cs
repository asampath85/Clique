using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Http;
using UserProfilerModel;
using UserProfilerService;

namespace UserProfilerWeb.Controllers
{
    public class RequestAPIController : ApiController
    {
      
      
        [HttpGet]
        public int GetLocation(string id)
        {
            RequestService service = new RequestService();
            return service.GetLocation(id);
        }

        [HttpGet]
        public int GetUser(string id)
        {
            RequestService service = new RequestService();
            return service.GetUser(id);
        }

        [HttpGet]
        public IList<RequestModel> GetRequest()
        {

            RequestService service = new RequestService();
            return service.GetRequest(RequestStatus.All);
        }

        [HttpGet]
        public IList<CliqueClaimRequestModel> GetClaimRequest()
        {

            RequestService service = new RequestService();
            return service.GetClaimRequest();
        }

        [HttpGet]
        public IList<EventModel> GetEvent(int id)
        {
            RequestService service = new RequestService();
            return service.GetEvent(id);
        }

        [HttpGet]
        public IList<TweetModel> GetLocationTweet(int id)
        {
            RequestService service = new RequestService();
            return service.GetLocationTweet(id);
        }

        [HttpGet]
        public IList<TweetModel> GetUserTweet(int id)
        {
            RequestService service = new RequestService();
            return service.GetUserTweet(id);
        }

        [HttpPost]
        public void AddClaimRequest(CliqueClaimRequestModel model)
        {

            RequestService service = new RequestService();
            var UserProfilerModel = service.AddClaimRequest(model);

            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue thumbnailRequestQueue = queueClient.GetQueueReference("addrequest");
            thumbnailRequestQueue.CreateIfNotExists();
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(new NewRequest { RequestId = UserProfilerModel.Id }));
            thumbnailRequestQueue.AddMessage(queueMessage);

        }

        [HttpPost]
        public void AddRequest(RequestModel model)
        {

            RequestService service = new RequestService();
            var UserProfilerModel = service.AddRequest(model);
            
            var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ToString());
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            CloudQueue thumbnailRequestQueue = queueClient.GetQueueReference("addrequest");
            thumbnailRequestQueue.CreateIfNotExists();
            var queueMessage = new CloudQueueMessage(JsonConvert.SerializeObject(new NewRequest { RequestId = UserProfilerModel.Id }));
            thumbnailRequestQueue.AddMessage(queueMessage);


        }

        [HttpPost]
        public void AddListing(ListModel model)
        {

            


        }
    }
}
