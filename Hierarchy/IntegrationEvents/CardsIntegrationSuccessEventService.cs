using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Saga;

namespace Hierarchy.IntegrationEvents
{
    public class CardsIntegrationSuccessEventService : ICardsIntegrationEventStatusService
    {
        private readonly IEventBus _eventBus;

        public CardsIntegrationSuccessEventService(IEventBus eventBus)
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
