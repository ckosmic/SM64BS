namespace SM64BS.Plugins.Interfaces
{
    public interface ISM64BSPlugin : IEventHandler
    {
        void PluginInitialize();
        void PluginDispose();
    }
}
