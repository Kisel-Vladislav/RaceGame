namespace CodeBase.Progress
{
    public interface ISavedProgressReader : ISavedProgress
    {
        public void LoadProgress(PlayerProgress progress);
    }
}
