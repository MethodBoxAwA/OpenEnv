using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenEnv
{
    internal class DataType
    {
        internal enum BehaviorType
        {
            Signal,
            OnStart,
            OnClock,
            OnTrigger,
            OnShutDown
        }
    }
}
