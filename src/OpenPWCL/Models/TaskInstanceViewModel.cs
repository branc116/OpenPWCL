using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenPWCL.Models
{
    [ToTypescript]
    public class TaskInstanceViewModel
    {
        public Guid InstanceId { get; set; }
        public string InputParamsJsno { get; set; }
    }

    public class ToTypescriptAttribute: Attribute
    {

    }
}
