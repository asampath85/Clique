using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserProfilerModel;
using UserProfilerService;

namespace HomeAwayWeb.Controllers
{
    public class HomeAwayAPIController : ApiController
    {
        [HttpPost]
        public void AddProperty(HomeAwayPropertyModel model)
        {           
            RequestService service = new RequestService();
            service.AddProperty(model);

        }

        [HttpPost]
        public void AddFeedback(UserFeedbackModel model)
        {
           
        }

        [HttpGet]
        public IList<HomeAwayPropertyModel> GetPropertyList()
        {
            RequestService service = new RequestService();
            return service.GetProperty();

            
        }

        [HttpGet]
        public HomeAwayPropertyModel GetProperty(int id)
        {
            RequestService service = new RequestService();
            return service.GetProperty(id).FirstOrDefault();


        } 
    }
}
