﻿using Cards.DBContext;
using Cards.Helper;
using Saga;
using Saga.Events;
using Saga.IntegrationEventLogEF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.IntegrationEvents
{
    public class CardsOrderRequestIntegrationSuccessEventHandler : IIntegrationEventHandler<CardOrderRequestIntegrationSuccessEventIntegrationEvent>
    {
        
        private readonly RIOCardsContext _cardsContext;
    
        public CardsOrderRequestIntegrationSuccessEventHandler(RIOCardsContext context)
        {
            _cardsContext = context;
           
        }


        public async Task Handle(CardOrderRequestIntegrationSuccessEventIntegrationEvent @event)
        {
            try
            {
                var entity = _cardsContext.Cards.FirstOrDefault(item => item.CorrelationId == @event.CorrelationID);
                if (entity != null)
                {
                    entity.TransactionStatus = (int)TransactionStatusEnum.Commited;
                    _cardsContext.Cards.Update(entity);
                    _cardsContext.SaveChanges();
                }
                var Evententity = _cardsContext.IntegrationEventLog.FirstOrDefault(item => item.CorrelationId == @event.CorrelationID);
                if (Evententity != null)
                {
                    Evententity.State = (int)EventStateEnum.Success;
                    _cardsContext.IntegrationEventLog.Update(Evententity);
                    _cardsContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
            }

        }

       
        public async Task CompletionHandle(CardOrderRequestIntegrationSuccessEventIntegrationEvent @event)
        {
            try
            {

            }
            catch(Exception ex)
            {

            }
        }
    }
}
