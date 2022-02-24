namespace SM64BS.Plugins.Interfaces
{
    public interface IPluginEventHandler : IEventHandler
    {
        void PluginInitialize();
        void PluginDispose();
    }
}
