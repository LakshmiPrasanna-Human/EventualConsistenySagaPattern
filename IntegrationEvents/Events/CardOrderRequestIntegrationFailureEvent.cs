using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Events
{
    public class CardOrderRequestIntegrationFailureEvent : IntegrationEvent
    {

        public string TransactionStatus { get; set; }

        public string CorrelationID { get; set; }

        public CardOrderRequestIntegrationFailureEvent(string transactionstatus, string correlationid)
        {
            TransactionStatus = transactionstatus;
            CorrelationID = correlationid;


        }
    }
}
