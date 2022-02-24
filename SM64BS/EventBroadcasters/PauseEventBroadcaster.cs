using SM64BS.Plugins.Interfaces;

namespace SM64BS.EventBroadcasters
{
    internal class PauseEventBroadcaster : EventBroadcaster<IPauseEventHandler>
    {
        private PauseController _pauseController;

        public PauseEventBroadcaster(PauseController pauseController)
        {
            _pauseController = pauseController;
        }

        public override void Initialize()
        {
            _pauseController.didPauseEvent += DidPauseHandler;
            _pauseController.didResumeEvent += DidUnpauseHandler;
        }

        public override void Dispose()
        {
            _pauseController.didPauseEvent -= DidPauseHandler;
            _pauseController.didResumeEvent -= DidUnpauseHandler;
        }

        private void DidPauseHandler()
        {
            foreach (IPauseEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.DidPause();
            }
        }

        private void DidUnpauseHandler()
        {
            foreach (IPauseEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.DidUnpause();
            }
        }
    }
}
