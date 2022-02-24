using SM64BS.Attributes;
using SM64BS.Managers;
using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.Plugins
{
    public abstract class SM64BSPlugin : IPluginEventHandler
    {
        [Inject] protected SM64BSGame GameScene;

        public abstract void PluginInitialize();
        public abstract void PluginDispose();
    }
}
