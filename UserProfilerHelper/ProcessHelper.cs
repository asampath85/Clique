using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfilerModel;
using UserProfilerService;

namespace UserProfilerHelper
{
    public class ProcessHelper
    {
        public bool ProcessPendingRequests(int requestId)
        {
           
                var service = new RequestService();
                var requestist = service.GetClaimRequest(requestId);
           

            try {

                EventHelper.ProcessPendingEvents(requestist);
            }
            catch (Exception)
            {
                return false;
            }

            try {

              //  UserHelper.ProcessPendingUsers(requestist);
            }
            catch (Exception)
            {
                return false;
            }
            try
            {                
                LocationHelper.ProcessPendingLocation(requestist);
            }
            catch (Exception)
            {
                return false;
            }
            try
            {
                WeatherHelper.ProcessLocation(requestist);
            }
            catch (Exception)
            {
                return false;
            }


            return true;


            
        }
    }
}
