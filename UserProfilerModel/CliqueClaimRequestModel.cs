using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class CliqueClaimRequestModel
    {
        public double Score { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }
        public string   StatusName { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Bedrooms { get; set; }
        public int Beds { get; set; }
        public int Accomodates { get; set; }
        public string Type { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public Nullable<int> NightPrice { get; set; }
        public Nullable<int> WeekPrice { get; set; }
        public string SSN { get; set; }
        public string Locality { get; set; }
        public System.DateTime AddedAt { get; set; }
        public bool IsPetsAllowed { get; set; }
        public bool IsWifiAvailable { get; set; }
        public bool IsACAvailable { get; set; }
        public bool IsLiftAvailable { get; set; }
        public bool IsPrivatePoolAvailable { get; set; }
        public bool IsBuzzerAvailable { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

    }
}
