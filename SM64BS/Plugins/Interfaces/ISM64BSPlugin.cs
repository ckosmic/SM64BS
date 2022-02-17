using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Plugins.Interfaces
{
    public interface ISM64BSPlugin : IEventHandler
    {
        void PluginInitialize();
        void PluginDispose();
    }
}
