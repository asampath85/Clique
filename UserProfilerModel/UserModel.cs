using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserProfilerModel

{
    public class UserModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string TwitterUserName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime ModifiedAt { get; set; }
        public Nullable<decimal> Score { get; set; }

    }
}
