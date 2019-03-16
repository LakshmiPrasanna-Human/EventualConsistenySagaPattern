using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Users
{
    public class UsersSettings
    {
        public string serviceBusConnectionString { get; set; }

        public bool UseCustomizationData { get; set; }
        public bool AzureStorageEnabled { get; set; }

        public string EventBusConnection { get; set; }
    }
}
