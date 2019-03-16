using System;
using System.Data.Common;
using System.Threading.Tasks;
using Hierarchy.DBContext;
using Saga;
using Saga.IntegrationEventLogEF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Hierarchy.DBContext;

namespace Hierarchy.IntegrationEvents
{
    public class CardsIntegrationEventService : ICardsIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly RIOHierarchyContext _hierarchyContext;
        private readonly IIntegrationEventLogService _eventLogService;

        public CardsIntegrationEventService(IEventBus eventBus, RIOHierarchyContext hierarchyContext,
        Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory)
        {
            _hierarchyContext = hierarchyContext ?? throw new ArgumentNullException(nameof(hierarchyContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_hierarchyContext.Database.GetDbConnection());
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
            await ResilientTransaction.New(_hierarchyContext)
                .ExecuteAsync(async () =>
                {
                     // Achieving atomicity between original catalog database operation and the IntegrationEventLog thanks to a local transaction
                     await _hierarchyContext.SaveChangesAsync();
                     await _eventLogService.SaveEventAsync(evt, _hierarchyContext.Database.CurrentTransaction.GetDbTransaction());
                });
            //send event message CardOrderRequestIntegrationSuccessEventIntegrationEvent
        }
    }





}
