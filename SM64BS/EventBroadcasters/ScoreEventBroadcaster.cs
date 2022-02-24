using SM64BS.Plugins.Interfaces;

namespace SM64BS.EventBroadcasters
{
    internal class ScoreEventBroadcaster : EventBroadcaster<IScoreEventHandler>
    {
        private ScoreController _scoreController;

        public ScoreEventBroadcaster(ScoreController scoreController)
        {
            _scoreController = scoreController;
        }

        public override void Initialize()
        {
            _scoreController.multiplierDidChangeEvent += MultiplierDidChangeHandler;
            _scoreController.scoreDidChangeEvent += ScoreDidChangeHandler;
            _scoreController.comboDidChangeEvent += ComboDidChangeHandler;
            _scoreController.noteWasCutEvent += NoteWasCutHandler;
            _scoreController.noteWasMissedEvent += NoteWasMissedHandler;
        }

        public override void Dispose()
        {
            _scoreController.multiplierDidChangeEvent -= MultiplierDidChangeHandler;
            _scoreController.scoreDidChangeEvent -= ScoreDidChangeHandler;
            _scoreController.comboDidChangeEvent -= ComboDidChangeHandler;
            _scoreController.noteWasCutEvent -= NoteWasCutHandler;
            _scoreController.noteWasMissedEvent -= NoteWasMissedHandler;
        }

        private void MultiplierDidChangeHandler(int multiplier, float progress)
        {
            foreach (IScoreEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.MultiplierDidChange(multiplier, progress);
            }
        }

        private void ScoreDidChangeHandler(int score, int scoreAfterModifier)
        {
            foreach (IScoreEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.ScoreDidChange(score, scoreAfterModifier);
            }
        }

        private void ComboDidChangeHandler(int combo)
        {
            foreach (IScoreEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.ComboDidChange(combo);
            }
        }

        private void NoteWasCutHandler(NoteData noteData, in NoteCutInfo noteCutInfo, int multiplier)
        {
            foreach (IScoreEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteWasCut(noteData, noteCutInfo, multiplier);
            }
        }

        private void NoteWasMissedHandler(NoteData noteData, int multiplier)
        {
            foreach (IScoreEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.NoteWasMissed(noteData, multiplier);
            }
        }
    }
}
