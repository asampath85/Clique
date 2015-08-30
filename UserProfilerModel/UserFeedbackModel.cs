using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class UserFeedbackModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Feedback { get; set; }
        public float Score { get; set; }
        public string ZipCode { get; set; }
        public string BuildingName { get; set; }

    }
}
