using Agava.YandexGames;
using Assets.CodeBase.Services;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.PausedService;
using CodeBase.Infrastructure.Service.PlayerProxy;
using CodeBase.Infrastructure.Service.Wallet;
using CodeBase.Progress;
using CodeBase.Services.Ad;
using CodeBase.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Initial";

        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices();
        }

        public void Enter()
        {
            _sceneLoader.Load(Initial, EnterLoadLevel);
        }
        public void Exit()
        {
        }

        private void EnterLoadLevel() =>
            _gameStateMachine.Enter<LoadProgressState>();
        private void RegisterServices()
        {
            RegisterAdService();
            RegisterPlaterProxyService();
            _services.RegisterSingle<IInputService>(new StandardInputService());
            _services.RegisterSingle<IPausedService>(new PausedService());
            _services.RegisterSingle<IAssetProvider>(new AssetProvider());
            RegisterStaticData();
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAssetProvider>(), _services.Single<IStaticDataService>(), _services.Single<IPausedService>()));
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IDataProvider>(new LocalDataProvider(_services.Single<IPersistentProgressService>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IDataProvider>()));
            _services.RegisterSingle<IWallet>(new Wallet());
        }

        private void RegisterPlaterProxyService()
        {
            if (YandexGamesSdk.IsRunningOnYandex)
                _services.RegisterSingle<IPlayerProxyService>(new YandexPlayerProxyService());
            else
                _services.RegisterSingle<IPlayerProxyService>(new SpyPlayerProxyService());
        }
        private void RegisterAdService()
        {
            IAdService adService;

            if (YandexGamesSdk.IsRunningOnYandex)
                adService = new YandexAdService();
            else
                adService = new SpyAdService();

            _services.RegisterSingle(adService);

        }
        private void RegisterStaticData()
        {
            var staticDataService = new StaticDataService();
            staticDataService.LoadCars();
            staticDataService.LoadLevel();
            _services.RegisterSingle<IStaticDataService>(staticDataService);
        }
    }
}
