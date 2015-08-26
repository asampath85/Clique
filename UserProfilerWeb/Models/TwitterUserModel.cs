using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserProfilerWeb.Models
{
    public class TwitterUserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string ImageURL { get; set; }
    }
}