using LibSM64;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.EventBroadcasters
{
    internal class PluginEventBroadcaster : EventBroadcaster<ISM64BSPlugin>
    {
        public override void Initialize()
        {
            foreach (ISM64BSPlugin plugin in EventHandlers)
            {
                plugin.PluginInitialize();
            }
        }

        public override void Dispose()
        {
            foreach (ISM64BSPlugin plugin in EventHandlers)
            {
                plugin.PluginDispose();
            }
        }
    }
}
