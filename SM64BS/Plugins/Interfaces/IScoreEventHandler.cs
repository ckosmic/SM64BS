namespace SM64BS.Plugins.Interfaces
{
    public interface IScoreEventHandler : IEventHandler
    {
        void MultiplierDidChange(int multiplier, float progress);
        void ScoreDidChange(int score, int scoreAfterModifier);
        void ComboDidChange(int combo);
        void NoteWasCut(NoteData noteData, in NoteCutInfo noteCutInfo, int multiplier);
        void NoteWasMissed(NoteData noteData, int multiplier);
    }
}
