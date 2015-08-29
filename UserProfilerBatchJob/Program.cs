using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserProfilerHelper;
using UserProfilerModel;

namespace UserProfilerBatchJob
{
    class Program
    {
        static void Main(string[] args)
        {
            ProcessHelper helper = new ProcessHelper();

            //Pass the id which u  want to test
            helper.ProcessPendingRequests(2);
        }
    }
}
