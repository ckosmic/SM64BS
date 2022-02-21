using SM64BS.Plugins.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BeatmapSaveData;

namespace SM64BS.EventBroadcasters
{
    internal class BeatmapEventBroadcaster : EventBroadcaster<IBeatmapEventHandler>
    {
        private BeatmapObjectManager _beatmapObjectManager;
        private BeatmapObjectCallbackController _beatmapObjectCallbackController;

        public BeatmapEventBroadcaster(BeatmapObjectManager beatmapObjectManager, BeatmapObjectCallbackController beatmapObjectCallbackController)
        {
            _beatmapObjectManager = beatmapObjectManager;
            _beatmapObjectCallbackController = beatmapObjectCallbackController;
        }

        public override void Initialize()
        {
            _beatmapObjectCallbackController.beatmapEventDidTriggerEvent += BeatmapEventTriggeredHandler;
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
