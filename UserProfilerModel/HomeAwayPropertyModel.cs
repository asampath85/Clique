using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class HomeAwayPropertyModel
    {

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
        public int NightPrice { get; set; }
        public int WeekPrice { get; set; }
        public System.DateTime AddedAt { get; set; }
        public string Locality { get; set; }

        //TODO: need to add amenity

    }
}
