using Assets.CodeBase.Services;
using CodeBase.Infrastructure.States;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        public static Game Instance { get; private set; }

        public Game(ICoroutineRunner coroutineRunner, ICurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), AllServices.Container, curtain);
            Instance = this;
        }
    }
}