using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel
{
    public class TweetModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string PostedBy { get; set; }
        public string PostedAt { get; set; }
        public Nullable<double> Score { get; set; }
        public string ProfileImageURL { get; set; }
        public string TweetIdStr { get; set; }
        public int RequestId { get; set; }
       
    }
}
