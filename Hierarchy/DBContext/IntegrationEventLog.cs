using System;
using System.Collections.Generic;

namespace Hierarchy.DBContext
{
    public partial class IntegrationEventLog
    {
        public int Id { get; set; }
        public Guid EventId { get; set; }
        public string state { get; set; }
        public string IntegrationEvent { get; set; }
        public int? TimesSent { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Content { get; set; }
        public string EventTypeName { get; set; }
    }
}
