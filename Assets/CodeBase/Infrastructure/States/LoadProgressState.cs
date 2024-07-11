using CodeBase.Infrastructure.Service;

namespace CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IDataProvider _dataProvider;
        private readonly GameStateMachine _gameStateMachine;

        public LoadProgressState(GameStateMachine gameStateMachine, IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _dataProvider.LoadOrInitNew();
            _gameStateMachine.Enter<LoadMainMenuState>();
        }
        public void Exit()
        {
        }
    }
}
