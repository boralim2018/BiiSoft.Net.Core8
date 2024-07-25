using Hangfire.Common;
using Hangfire.States;
using System;

namespace BiiSoft.HangfireFilter
{
    /// <summary>
    /// Use this attribute to auto delete job after running success. 
    /// But don't use this attribute with ContinueWithJob 
    /// becuase if parent job is delete all children jobs will automatically delete without excecuted.   
    /// </summary>
    /// <param name="context"></param>
    public class DeleteOnSuccessFilter : JobFilterAttribute, IElectStateFilter
    {        
        public void OnStateElection(ElectStateContext context)
        {
            if (context.CandidateState.Name == SucceededState.StateName)
            {
                context.CandidateState = new DeletedState
                {
                    Reason = "Deleted automatically when succeeded."
                };
            }
        }
    }
}
