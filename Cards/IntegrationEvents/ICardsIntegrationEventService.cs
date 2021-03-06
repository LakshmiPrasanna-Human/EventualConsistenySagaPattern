﻿using Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards.IntegrationEvents
{
    public interface ICardsIntegrationEventService
    {
        Task SaveEventAndCardsContextChangesAsync(IntegrationEvent evt);
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
