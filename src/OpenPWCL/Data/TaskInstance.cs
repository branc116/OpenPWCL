using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPWCL.Data
{
    public class TaskInstance
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string InputParams { get; set; }
        public bool Finished { get; set; }
        public string Resoult { get; set; }
    }
}
