//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserProfilerCommon
{
    using System;
    using System.Collections.Generic;
    
    public partial class CliqueClaimRequest
    {
        public CliqueClaimRequest()
        {
            this.CliqueLocationEvents = new HashSet<CliqueLocationEvent>();
            this.CliqueLocationTweets = new HashSet<CliqueLocationTweet>();
        }
    
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
        public Nullable<System.DateTime> AddedAt { get; set; }
        public Nullable<bool> IsPetsAllowed { get; set; }
        public Nullable<bool> IsWifiAvailable { get; set; }
        public Nullable<bool> IsACAvailable { get; set; }
        public Nullable<bool> IsLiftAvailable { get; set; }
        public Nullable<bool> IsPrivatePoolAvailable { get; set; }
        public Nullable<bool> IsBuzzerAvailable { get; set; }
        public Nullable<double> Score { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> Status { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
    
        public virtual ICollection<CliqueLocationEvent> CliqueLocationEvents { get; set; }
        public virtual ICollection<CliqueLocationTweet> CliqueLocationTweets { get; set; }
    }
}
