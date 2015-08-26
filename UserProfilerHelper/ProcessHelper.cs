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
        public bool ProcessPendingRequests()
        {
           
                var service = new RequestService();
                var requestist = service.GetRequest(RequestStatus.Queue);
           

            try {

                EventHelper.ProcessPendingEvents(requestist);
            }
            catch (Exception)
            {
                return false;
            }

            try {

                UserHelper.ProcessPendingUsers(requestist);
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

            return true;


            
        }
    }
}
