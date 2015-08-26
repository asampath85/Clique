using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfilerModel
{
    public class RequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int LocationId { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public string BuildingName { get; set; }
        public string Address { get; set; }
        public decimal Score { get; set; }

        
        public string City { get; set; }
        public string Pincode { get; set; }

        public string UserName { get; set; }
        public string TwitterUserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }

        public string StatusName { get; set; }
    }

    public enum RequestStatus
    {
        New,
        Queue,
        Processed,
        Error,
        All
    }
}