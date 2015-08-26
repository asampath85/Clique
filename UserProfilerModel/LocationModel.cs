using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class LocationModel
    {

        public int Id { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }
        public Nullable<decimal> Score { get; set; }
    }
}
