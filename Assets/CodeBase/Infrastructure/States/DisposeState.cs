using CodeBase.Infrastructure.Service;

namespace CodeBase.Infrastructure.States
{
    public class DisposeState : IState
    {
        private readonly ISaveLoadService _saveLoadService;

        public DisposeState(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Enter() => 
            _saveLoadService.InformProgressWriters();
        public void Exit()
        {
        }
    }
}
