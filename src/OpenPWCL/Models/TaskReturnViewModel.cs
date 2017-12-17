using System;

namespace OpenPWCL.Models
{
    [ToTypescript]
    public class TaskReturnViewModel
    {
        public Guid TaskId { get; set; }
        public Guid TaskInstanceId { get; set; }
        public string ReturnJson { get; set; }
        public Status Status { get; set; }
    }

    [ToTypescript]
    [Flags]
    public enum Status
    {
        Nothing = 1,
        More = 2,
        NoMore = 4,
        Error = 8
    }
}