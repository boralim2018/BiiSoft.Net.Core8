using Abp.Dependency;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Tests
{
    public class TestHangfireManager: ITestHangfireManager, ITransientDependency
    {
        public TestHangfireManager() { 
        
        }

        public async Task TestMethod()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Method Exceted");
                //throw new Exception("Exception Test Max Retry");
            });
        }
    }

}
