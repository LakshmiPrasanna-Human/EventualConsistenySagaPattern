using Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hierarchy.IntegrationEvents
{
    public interface ICardsIntegrationEventStatusService
    {
        Task PublishThroughEventBus(IntegrationEvent evt);
    }
}
