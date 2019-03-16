using Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users.IntegrationEvents
{
    public interface ICardsIntegraionEventStatusService
    {
        Task PublishThroughEventBus(IntegrationEvent evt);
    }
}
