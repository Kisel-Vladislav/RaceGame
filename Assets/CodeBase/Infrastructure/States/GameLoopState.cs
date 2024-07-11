using CodeBase.Infrastructure.Service;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly ISaveLoadService _saveLoadService;

        public GameLoopState(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
        }
        public void Exit()
        {
            _saveLoadService.InformProgressWriters();
            _saveLoadService.CleanupCode();
        }
    }
}
