using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Saga
{
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
