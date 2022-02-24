using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM64BS.Plugins.Interfaces
{
    public interface IBeatmapEventHandler : IEventHandler
    {
        void BeatmapEventTriggered(BeatmapEventData songEvent);
        void NoteWasSpawned(NoteData noteData);
        void NoteWasDespawned(NoteData noteData);
        void NoteWasMissed(NoteData noteData);
        void NoteWasCut(NoteData noteData, NoteCutInfo noteCutInfo);
        void NoteDidStartJump(NoteData noteData);
        void ObstacleWasSpawned(ObstacleData obstacleData);
        void ObstacleWasDespawned(ObstacleData obstacleData);
        void ObstacleDidPassAvoidedMark(ObstacleData obstacleData);
    }
}
