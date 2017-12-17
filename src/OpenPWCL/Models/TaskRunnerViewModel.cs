using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPWCL.Models
{
    public class TaskInitViewModel
    {
        public TaskInitViewModel()
        {
            TaskName = "Test";
            NewTaskUri = "/Tests/GetTaskToSolve";
            ReturnUri = "/TaskInstances/TaskInstanceSolved";
        }
        public Guid TaskId { get; set; }
        public string TaskName { get; set; }
        public string NewTaskUri { get; set; }
        public string ReturnUri { get; set; }
    }
}
