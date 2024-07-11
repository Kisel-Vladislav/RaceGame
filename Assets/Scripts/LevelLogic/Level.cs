using Agava.YandexGames;
using CodeBase.Car;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Service.PausedService;
using CodeBase.Infrastructure.Service.Wallet;
using CodeBase.Infrastructure.States;
using CodeBase.Progress;
using Scripts.UI;
using System;
using UnityEngine;

namespace Scripts.LevelLogic
{
    public class Level : MonoBehaviour, ISavedProgressReader
    {
        private const float k = 0.2f;
        private const float _baseReward = 2500;

        [SerializeField] private TimerCountdown _timer;
        [SerializeField] private RaceResultsPanel _raceResultsPanel;

        public bool IsCompleted { get; private set; }

        public FinishLine FinishLine;
        public CarSpawner CarSpawner;
        public Stopwatch Stopwatch;

        private IPausedService _pausedService;
        private LevelId _levelId;
        private GameStateMachine _gameStateMachine;
        private IWallet _wallet;


        private float _bestTimeInSeconds;
        public void Construct(LevelId levelId, GameStateMachine gameStateMachine, IWallet wallet, IPausedService pausedService)
        {
            _levelId = levelId;

            _gameStateMachine = gameStateMachine;
            _wallet = wallet;
            _pausedService = pausedService;

            Stopwatch.Construct(this);
        }
        public void Started()
        {
            _pausedService.SetPaused(true);
            _timer.OnCompleted += LevelStart;
            FinishLine.OnFinished += LevelCompleted;
        }
        public void LevelCompleted()
        {
            IsCompleted = true;

            var reward = CalculateReward();
            _wallet.AddMoney(reward);

            if(YandexGamesSdk.IsRunningOnYandex)
            {
                Leaderboard.GetPlayerEntry(_levelId.ToString(),
                    (data) =>
                    {
                        if (data.score < reward)
                            Leaderboard.SetScore(_levelId.ToString(), reward);
                    });
            }

            _raceResultsPanel.Show(Stopwatch.CurrentTimeInSeconds, _bestTimeInSeconds, reward);
            Invoke(nameof(ReturnToMainMenu), 10);
        }

        private bool IsCurrentResultBetter =>
            _bestTimeInSeconds > Stopwatch.CurrentTimeInSeconds;
        private bool IsFirstTry =>
            _bestTimeInSeconds == 0;

        private void LevelStart()
        {
            _pausedService.SetPaused(false);
            Stopwatch.StartStopWatch();
        }
        private int CalculateReward() =>
            (int)(_baseReward / Math.Pow(Stopwatch.CurrentTimeInSeconds, k));
        private void ReturnToMainMenu() =>
            _gameStateMachine.Enter<LoadMainMenuState>();

        public void LoadProgress(PlayerProgress progress)
        {
            foreach (var levelData in progress.LevelData)
            {
                if (levelData.Id == _levelId)
                {
                    _bestTimeInSeconds = levelData.BestLopTimeInSeconds;
                }
            }

        }
        public void SaveProgress(PlayerProgress progress)
        {
            foreach (var levelData in progress.LevelData)
            {
                if (levelData.Id == _levelId && IsFirstTry || IsCurrentResultBetter)
                {
                    levelData.BestLopTimeInSeconds = Stopwatch.CurrentTimeInSeconds;
                    Debug.Log($"Save({levelData.Id == _levelId && IsFirstTry || IsCurrentResultBetter}) {levelData.BestLopTimeInSeconds}");
                }
            }
        }
    }
}