using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service;
using CodeBase.Infrastructure.Service.PausedService;
using CodeBase.Infrastructure.Service.Wallet;
using CodeBase.Progress;
using CodeBase.StaticData;
using Scripts.LevelLogic;
using Scripts.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<LevelStaticData>
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ICurtain _curtain;
        private readonly IWallet _wallet;
        private readonly IPausedService _pausedService;
        private LevelStaticData _levelData;
        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, IGameFactory gameFactory, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoadService, IWallet wallet, ICurtain curtain, IPausedService pausedService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _curtain = curtain;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _wallet = wallet;
            _pausedService = pausedService;
        }

        public void Enter(LevelStaticData levelData)
        {
            _curtain.Show();
            _levelData = levelData;
            _sceneLoader.Load(levelData.SceneName, OnLoaded);
        }
        public void Exit()
        {
            _curtain.Hide();
        }
        private void OnLoaded()
        {
            var level = InitLevel();
            InitGameWorld(level);
            InitHUD(level);

            _gameStateMachine.Enter<GameLoopState>();

            RegisterProgressReader(_wallet, level);
            _saveLoadService.InformProgressReaders();
        }

        private void InitGameWorld(Level level)
        {
            InitCarSpawner(level);
            InitFinishLine(level);
            InitRaceBarrier();
            level.Started();

            InitWayPoints();
        }
        private Level InitLevel()
        {
            var level = Object.FindFirstObjectByType<Level>();
            level.Construct(_levelData.LevelId, _gameStateMachine, _wallet,_pausedService);
            return level;

        }
        private void InitWayPoints() =>
            Object.Instantiate(_levelData.Waypoints);
        private void InitFinishLine(Level level) =>
            level.FinishLine.Construct(_gameFactory.CreateLapObjectives(_levelData.LevelId));
        private void InitRaceBarrier()
        {
            foreach (var barrier in _levelData.RaceBarrier)
                Object.Instantiate(barrier);
        }
        private void InitCarSpawner(Level level)
        {
            level.CarSpawner.Construct(_gameFactory, _persistentProgressService.PlayerProgress.PlayerCars.CurrentCar, _levelData.Waypoints.Waypoints);
            level.CarSpawner.Spawn();
        }
        private void InitHUD(Level level)
        {
            var hud = _gameFactory.CreateHUD();
            level.Stopwatch.TimeDisplay = hud.GetComponentInChildren<TimeDisplay>();
        }
        private void RegisterProgressReader(params ISavedProgress[] savedProgresses)
        {
            foreach (var item in savedProgresses)
                _saveLoadService.Register(item);
        }
    }
}
