﻿using System;
using System.Collections.Generic;

namespace Cards.DBContext
{
    public partial class IntegrationEventLog
    {
        public int Id { get; set; }
        public string CorrelationId { get; set; }
        public Guid EventId { get; set; }
        public int? State { get; set; }
        public string IntegrationEvent { get; set; }
        public int? TimesSent { get; set; }
        public DateTime? CreationTime { get; set; }
        public string Content { get; set; }
        public string EventTypeName { get; set; }
    }
}
