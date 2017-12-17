using System;

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