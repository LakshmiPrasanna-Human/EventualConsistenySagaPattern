using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cards
{
    public class CardsSettings
    {
       public string EventBusConnection { get; set; }

        public bool UseCustomizationData { get; set; }
        public bool AzureStorageEnabled { get; set; }
    }
}
