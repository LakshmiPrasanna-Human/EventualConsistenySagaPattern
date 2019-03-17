using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Events
{
    public class CardOrderRequestIntegrationFailureEventIntegrationEvent : IntegrationEvent
    {

        public string TransactionStatus { get; set; }

        public string CorrelationID { get; set; }

        public CardOrderRequestIntegrationFailureEventIntegrationEvent(string transactionstatus, string correlationid)
        {
            TransactionStatus = transactionstatus;
            CorrelationID = correlationid;


        }
    }
}
