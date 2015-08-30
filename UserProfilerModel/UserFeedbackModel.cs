using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class UserFeedbackModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int PropertyId { get; set; }
        public System.DateTime AddedAt { get; set; }
        public double Score { get; set; }

    }
}
