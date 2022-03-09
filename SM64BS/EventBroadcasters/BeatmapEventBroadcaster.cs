using SM64BS.Plugins.Interfaces;

namespace SM64BS.EventBroadcasters
{
    internal class BeatmapEventBroadcaster : EventBroadcaster<IBeatmapEventHandler>
    {
        private BeatmapObjectManager _beatmapObjectManager;

        public BeatmapEventBroadcaster(BeatmapObjectManager beatmapObjectManager)
        {
            _beatmapObjectManager = beatmapObjectManager;
        }

        public override void Initialize()
        {
            _beatmapObjectManager.noteWasSpawnedEvent += NoteWasSpawnedHandler;
            _beatmapObjectManager.noteWasDespawnedEvent += NoteWasDespawnedHandler;
            _beatmapObjectManager.noteWasMissedEvent += NoteWasMissedHandler;
            _beatmapObjectManager.noteWasCutEvent += NoteWasCutHandler;
            _beatmapObjectManager.noteDidStartJumpEvent += NoteDidStartJumpHandler;
            _beatmapObjectManager.obstacleWasSpawnedEvent += ObstacleWasSpawnedHandler;
            _beatmapObjectManager.obstacleWasDespawnedEvent += ObstacleWasDespawnedHandler;
            _beatmapObjectManager.obstacleDidPassAvoidedMarkEvent += ObstacleDidPassAvoidedMarkHandler;
        }

        public override void Dispose()
        {
            
        }

        // 1.20 happened !!
        private void BeatmapEventTriggeredHandler(BeatmapEventData songEvent)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.BeatmapEventTriggered(songEvent);
            }
        }

        private void NoteWasSpawnedHandler(NoteController noteController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteWasSpawned(noteController.noteData);
            }
        }

        private void NoteWasDespawnedHandler(NoteController noteController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteWasDespawned(noteController.noteData);
            }
        }

        private void NoteWasMissedHandler(NoteController noteController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteWasMissed(noteController.noteData);
            }
        }

        private void NoteWasCutHandler(NoteController noteController, in NoteCutInfo noteCutInfo)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteWasCut(noteController.noteData, noteCutInfo);
            }
        }

        private void NoteDidStartJumpHandler(NoteController noteController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteDidStartJump(noteController.noteData);
            }
        }

        private void ObstacleWasSpawnedHandler(ObstacleController obstacleController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.ObstacleWasSpawned(obstacleController.obstacleData);
            }
        }

        private void ObstacleWasDespawnedHandler(ObstacleController obstacleController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.ObstacleWasDespawned(obstacleController.obstacleData);
            }
        }

        private void ObstacleDidPassAvoidedMarkHandler(ObstacleController obstacleController)
        {
            foreach (IBeatmapEventHandler eventHandler in EventHandlers)
{
                eventHandler?.ObstacleDidPassAvoidedMark(obstacleController.obstacleData);
            }
        }
    }
}
