using ManageSystem;
using System;
using System.Configuration;

namespace BoxSystem.Services
{
    public class ConfigItems : IConfigItems
    {
        public uint MaxItemsCount { get; set; }
        public uint MinItemsCount { get; set; }
        public double MaxDeviationAllowed { get; set; }
        public uint MaxSplitsAllowed { get; set; }
        public TimeSpan DaysBeforeOld { get; set; }

        public ConfigItems()
        {
            MaxItemsCount = uint.Parse(ConfigurationManager.AppSettings["maxIemsCount"]);
            MinItemsCount = uint.Parse(ConfigurationManager.AppSettings["minIemsCount"]);
            MaxDeviationAllowed = double.Parse(ConfigurationManager.AppSettings["maxDeviationAllowed"]);
            MaxSplitsAllowed = uint.Parse(ConfigurationManager.AppSettings["maxSplitsAllowed"]);
            DaysBeforeOld = new TimeSpan(int.Parse(ConfigurationManager.AppSettings["daysBeforeOld"]), 0, 0, 0);
        }
    }
}
