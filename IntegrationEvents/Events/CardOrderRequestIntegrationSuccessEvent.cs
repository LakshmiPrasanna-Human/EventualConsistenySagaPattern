using System;
using System.Collections.Generic;
using System.Text;

namespace Saga.Events
{
    public class CardOrderRequestIntegrationSuccessEventIntegrationEvent : IntegrationEvent
    {
        
        public string TransactionStatus { get; set; }

       // public string CorrelationID { get; set; }

        public CardOrderRequestIntegrationSuccessEventIntegrationEvent(string transactionstatus, string correlationid)
        {
            TransactionStatus = transactionstatus;
            CorrelationID = correlationid;
            
           
        }
    }
}
