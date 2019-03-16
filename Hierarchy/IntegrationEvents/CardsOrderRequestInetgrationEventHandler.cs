using Hierarchy.DBContext;
using Saga;
using Saga.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hierarchy.IntegrationEvents
{
    public class CardsOrderRequestIntegrationEventHandler : IIntegrationEventHandler<CardOrderRequestIntegrationEvent>
    {
        //  private readonly IBasketRepository _repository;
        private readonly RIOHierarchyContext _hierarchyContext;
        private readonly ICardsIntegrationEventService _cardsIntegrationEventService;
        private readonly ICardsIntegrationEventStatusService _cardsIntegrationEventStatusService;

        public CardsOrderRequestIntegrationEventHandler(RIOHierarchyContext context, ICardsIntegrationEventService cardsIntegrationEventService, ICardsIntegrationEventStatusService cardsIntegrationEventStatusService)
        {
            _hierarchyContext = context;
            _cardsIntegrationEventService = cardsIntegrationEventService;
            _cardsIntegrationEventStatusService = cardsIntegrationEventStatusService;
        }


        public async Task SuccessHandle(CardOrderRequestIntegrationEvent @event)
        {
            try
            {
                //handle CardsIntegrationSuccessEvent
                var cardorderrequestsuccessEvent = new CardOrderRequestIntegrationSuccessEventIntegrationEvent("SUCCESS", @event.CorrelationID);
                await _cardsIntegrationEventStatusService.PublishThroughEventBus(cardorderrequestsuccessEvent);

            }
            catch (Exception ex)
            {

            }

                
        }


        public async Task Handle(CardOrderRequestIntegrationEvent @event)
        {
            try
            {
                var item = new Hierarchy.DBContext.Invoice
                {
                    CardId = @event.UserId,
                    CorrelationId = @event.CorrelationID,
                    CompanyName = @event.CompanyName,
                    CardHolderName = @event.CardHolderName,
                };

                _hierarchyContext.Invoice.Add(item);
                var cardorderrequestEvent = new CardOrderRequestIntegrationEvent(@event.UserId, @event.CorrelationID, @event.CompanyName, @event.CardHolderName);
                await _cardsIntegrationEventService.SaveEventAndCardsContextChangesAsync(cardorderrequestEvent);

                //handle CardsIntegrationSuccessEvent
             //   var cardorderrequestsuccessEvent = new CardOrderRequestIntegrationSuccessEventIntegrationEvent("SUCCESS", @event.CorrelationID);
             //   await _cardsIntegrationEventStatusService.PublishThroughEventBus(cardorderrequestsuccessEvent);
                

            }
            catch(Exception ex)
            {
                //handle Send Failure to Source Microservice. -- CardOrderRequestIntegartionFailureEvent
                var cardorderrequestfailureEvent = new CardOrderRequestIntegrationFailureEvent("Failure", @event.CorrelationID);
                await _cardsIntegrationEventStatusService.PublishThroughEventBus(cardorderrequestfailureEvent);
               
            }

        }

       
    }
}
