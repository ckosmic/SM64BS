using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SM64BS.EventBroadcasters
{
    internal abstract class EventBroadcaster<T> : IInitializable, IDisposable where T : IEventHandler
    {
        [Inject] protected List<T> EventHandlers = new List<T>();

        public abstract void Initialize();
        public abstract void Dispose();
    }
}
