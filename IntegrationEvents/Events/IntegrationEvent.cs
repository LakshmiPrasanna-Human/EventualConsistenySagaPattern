﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Saga
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid id, DateTime createDate, string correlationid)
        {
            Id = id;
            CreationDate = createDate;
            CorrelationID = correlationid;
        }

        [JsonProperty]
        public string CorrelationID { get; set; }
        
        [JsonProperty]
        public Guid Id { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
