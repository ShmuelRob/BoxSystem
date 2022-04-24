using System;

namespace ManageSystemDll
{
    public class ConfigItems
    {
        public uint MaxItemsCount { get; set; }
        public uint MinItemsCount { get; set; }
        public double MaxDeviationAllowed { get; set; }
        public uint MaxSplitsAllowed { get; set; }
        public TimeSpan DaysBeforeOld { get; set; }

        public ConfigItems(uint maxItems, uint minItems, double maxDeviation, uint maxSplitsAllowed, TimeSpan daysBeforeold)
        {
            MaxItemsCount = maxItems;
            MinItemsCount = minItems;
            MaxDeviationAllowed = maxDeviation;
            MaxSplitsAllowed = maxSplitsAllowed;
            DaysBeforeOld = daysBeforeold;
        }
    }
}
