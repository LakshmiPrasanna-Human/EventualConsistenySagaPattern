using Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.IntegrationEvents
{
    public class CardsIntegrationEventStatusService : ICardsIntegraionEventStatusService
    {
        private readonly IEventBus _eventBus;

        public CardsIntegrationEventStatusService(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));

        }

        public async Task PublishThroughEventBus(IntegrationEvent evt)
        {
            try
            {

                _eventBus.Publish(evt);

            }
            catch (Exception ex)
            {

            }
        }
    }
}
