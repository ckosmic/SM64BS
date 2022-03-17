namespace SM64BS.Plugins.Interfaces
{
    public interface IScoreEventHandler : IEventHandler
    {
        void MultiplierDidChange(int multiplier, float progress);
        void ScoreDidChange(int score, int scoreAfterModifier);
        void ScoringForNoteFinished(ScoringElement scoringElement);
    }
}
