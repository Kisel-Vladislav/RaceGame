using Agava.YandexGames;
using CodeBase.Infrastructure.States;
using System.Collections;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
        private Game _game;

        public LoadingCurtain Curtain;

        public Game Game { get => _game; }

        private void Awake()
        {
            StartCoroutine(Init());
            DontDestroyOnLoad(this);
        }
        private IEnumerator Init()
        {
            if (YandexGamesSdk.IsRunningOnYandex)
                yield return YandexGamesSdk.Initialize();

            YandexGamesSdk.CallbackLogging = true;
            _game = new Game(this, Curtain);
            _game.StateMachine.Enter<BootstrapState>();
        }

        private void OnApplicationQuit() => 
            Game.StateMachine.Enter<DisposeState>();
    }
}