using Assets.CodeBase.Services;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.PausedService;
using CodeBase.Infrastructure.Service.Wallet;
using CodeBase.Progress;
using CodeBase.Services.Ad;
using CodeBase.StaticData;
using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
    public class GameStateMachine
    {
        private readonly Dictionary<Type, IExcitableState> _states;

        private IExcitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, AllServices services, ICurtain curtain)
        {
            _states = new Dictionary<Type, IExcitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadProgressState)] = new LoadProgressState(this, services.Single<IDataProvider>()),
                [typeof(LoadMainMenuState)] = new LoadMainMenuState(services.Single<ISaveLoadService>(), services.Single<IStaticDataService>(), services.Single<IAdService>(), services.Single<IWallet>(), sceneLoader, curtain),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, services.Single<IGameFactory>(), services.Single<IPersistentProgressService>(), services.Single<ISaveLoadService>(), services.Single<IWallet>(), curtain,services.Single<IPausedService>()),
                [typeof(GameLoopState)] = new GameLoopState(services.Single<ISaveLoadService>()),
                [typeof(DisposeState)] = new DisposeState(services.Single<ISaveLoadService>()),
            };
        }
        public void Enter<TState>()
            where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState, TPayload>(TPayload payload)
            where TState : class, IPayloadedState<TPayload>
        {
            var state = ChangeState<TState>();

            state.Enter(payload);
        }
        private TState GetState<TState>()
            where TState : class, IExcitableState
        {
            return _states[typeof(TState)] as TState;
        }
        private TState ChangeState<TState>()
            where TState : class, IExcitableState
        {
            _activeState?.Exit();
            var state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}
