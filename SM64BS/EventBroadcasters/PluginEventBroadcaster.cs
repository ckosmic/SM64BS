using SM64BS.Plugins.Interfaces;

namespace SM64BS.EventBroadcasters
{
    internal class PluginEventBroadcaster : EventBroadcaster<IPluginEventHandler>
    {
        public override void Initialize()
        {
            foreach (IPluginEventHandler eventHandler in EventHandlers)
            {
                eventHandler.PluginInitialize();
            }
        }

        public override void Dispose()
        {
            foreach (IPluginEventHandler eventHandler in EventHandlers)
            {
                eventHandler.PluginDispose();
            }
        }
    }
}
