using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManageSystem
{
    public interface IConfigItems
    {
        uint MaxItemsCount { get; set; }
        uint MinItemsCount { get; set; }
        double MaxDeviationAllowed { get; set; }
        uint MaxSplitsAllowed { get; set; }
        TimeSpan DaysBeforeOld { get; set; }
    }
}
