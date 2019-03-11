using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hierarchy
{
    public class HierarchySettings
    {
       public string serviceBusConnectionString { get; set; }

        public bool UseCustomizationData { get; set; }
        public bool AzureStorageEnabled { get; set; }

        public string EventBusConnection { get; set; }
    }
}
