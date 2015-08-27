using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class ListModel
    {

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public int? NoOfBedrooms { get; set; }
        public int? NoofBeds { get; set; }
        public int? NoOfPeople { get; set; }

        public int? NightlyCost { get; set; }
        public int? WeeklyCost { get; set; }

        public bool IsPetsAllowed { get; set; }
        public bool IsWifiAvailable { get; set; }
        public bool IsACAvailable { get; set; }
        public bool IsLiftAvailable { get; set; }
        public bool IsPrivatePool { get; set; }
        public bool IsBuzzer { get; set; }


    
    }
}
