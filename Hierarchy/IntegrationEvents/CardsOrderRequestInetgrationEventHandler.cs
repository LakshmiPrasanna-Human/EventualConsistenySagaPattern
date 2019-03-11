using Hierarchy.DBContext;
using Saga;
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

        public CardsOrderRequestIntegrationEventHandler(RIOHierarchyContext context, ICardsIntegrationEventService cardsIntegrationEventService)
        {
            _hierarchyContext = context;
            _cardsIntegrationEventService = cardsIntegrationEventService;
        }


        public async Task Handle(CardOrderRequestIntegrationEvent @event)
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

        }

       
    }
}
