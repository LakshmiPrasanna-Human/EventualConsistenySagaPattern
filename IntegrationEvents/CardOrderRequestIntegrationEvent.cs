using System;
using System.Collections.Generic;
using System.Text;

namespace Saga
{
    public class CardOrderRequestIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; set; }

       // public string CorrelationID { get; set; }

        public string CompanyName { get; set; }

        public string CardHolderName { get; set; }
        public CardOrderRequestIntegrationEvent(string userId, string correlationid, string companyName, string cardholdername)
        {
            UserId = userId;
            CorrelationID = correlationid;
            CompanyName = companyName;
            CardHolderName = cardholdername;
        }
    }

   
}
