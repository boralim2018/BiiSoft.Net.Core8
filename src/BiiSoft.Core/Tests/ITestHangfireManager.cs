using BiiSoft.HangfireFilter;
using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiiSoft.Tests
{
    public interface ITestHangfireManager
    {
        //[AutomaticRetry(Attempts = 2)]
        //[AutomaticRetry(Attempts = 2, OnAttemptsExceeded = AttemptsExceededAction.Delete)] //delete after retry 2 times
        //[DeleteOnSuccessFilter] //Do not use this attribute with ContinueWithJob   
        Task TestMethod();
    }

}
