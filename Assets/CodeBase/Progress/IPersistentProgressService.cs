using Assets.CodeBase.Services;

namespace CodeBase.Progress
{
    public interface IPersistentProgressService : IService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}