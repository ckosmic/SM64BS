namespace SM64BS.Plugins.Interfaces
{
    public interface IPauseEventHandler : IEventHandler
    {
        void DidPause();
        void DidUnpause();
    }
}
