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
            //_scoreController.scoringForNoteFinishedEvent += ScoringForNoteFinishedHandler;
        }

        public override void Dispose()
        {
            _scoreController.multiplierDidChangeEvent -= MultiplierDidChangeHandler;
            _scoreController.scoreDidChangeEvent -= ScoreDidChangeHandler;
            //_scoreController.scoringForNoteFinishedEvent -= ScoringForNoteFinishedHandler;
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

        /*private void ScoringForNoteFinishedHandler(ScoringElement scoringElement)
        {
            foreach (IScoreEventHandler eventHandler in EventHandlers)
            {
                eventHandler?.ScoringForNoteFinished(scoringElement);
            }
        }*/
    }
}
