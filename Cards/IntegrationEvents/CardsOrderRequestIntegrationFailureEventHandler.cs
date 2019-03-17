using Cards.DBContext;
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
    public class CardsOrderRequestIntegrationFailureEventHandler : IIntegrationEventHandler<CardOrderRequestIntegrationFailureEventIntegrationEvent>
    {

        private readonly RIOCardsContext _cardsContext;

        public CardsOrderRequestIntegrationFailureEventHandler(RIOCardsContext context)
        {
            _cardsContext = context;

        }


        public async Task Handle(CardOrderRequestIntegrationFailureEventIntegrationEvent @event)
        {
            try
            {
                var entity = _cardsContext.Cards.FirstOrDefault(item => item.CorrelationId == @event.CorrelationID);

                if (entity != null)
                {
                    entity.TransactionStatus = (int)TransactionStatusEnum.RollBack;
                    _cardsContext.Cards.Update(entity);
                    _cardsContext.SaveChanges();
                }
                var Evententity = _cardsContext.IntegrationEventLog.FirstOrDefault(item => item.CorrelationId == @event.CorrelationID);
                if (Evententity != null)
                {
                    Evententity.State = (int)EventStateEnum.Failure;
                    _cardsContext.IntegrationEventLog.Update(Evententity);
                    _cardsContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {


            }

        }


        public async Task CompletionHandle(CardOrderRequestIntegrationFailureEventIntegrationEvent @event)
        {
            try
            {
                
            }
            catch (Exception ex)
            {

            }
        }
    }
}
