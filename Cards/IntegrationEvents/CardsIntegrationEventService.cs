using System;
using System.Data.Common;
using System.Threading.Tasks;
using Cards.DBContext;
using Saga;
using Saga.IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Cards.IntegrationEvents
{
    public class CardsIntegrationEventService : ICardsIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly RIOCardsContext _cardsContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public CardsIntegrationEventService(IEventBus eventBus, RIOCardsContext cardsContext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _cardsContext = cardsContext ?? throw new ArgumentNullException(nameof(cardsContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_cardsContext.Database.GetDbConnection());
        }



        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public async Task SaveEventAndCardsContextChangesAsync(IntegrationEvent evt)
        {
            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_cardsContext)
                .ExecuteAsync(async () =>
                {
                     // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                     await _cardsContext.SaveChangesAsync();
                     await _eventLogService.SaveEventAsync(evt, _cardsContext.Database.CurrentTransaction.GetDbTransaction());
                });
        }
    }





}
