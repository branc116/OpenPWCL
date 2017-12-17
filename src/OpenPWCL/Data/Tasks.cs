using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPWCL.Data
{
    public class Tasks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string JavascriptCode { get; set; }
        public string Priority { get; set; }
        public List<TaskInstance> TaskInstances { get; set; }
    }
}
