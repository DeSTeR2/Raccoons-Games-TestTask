using Infrastructure.Event;
using Infrastructure.Events;
using Infrastructure.Game;
using Infrastructure.StateMachine.States;

namespace Infrastructure.StateMachine
{
    public class GameState : IState
    {
        private const string GameScene = "GameScene";
        private readonly EventSO _gameSceneLoaded;
        private readonly GameStateMachine _gameStateMachine;

        public GameState(GameStateMachine gameStateMachine, AllEvents allEvents)
        {
            _gameStateMachine = gameStateMachine;
            _gameSceneLoaded = allEvents.Find<LevelEvent>().gameSceneLoaded;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadingState, string>(Scenes.GameScene, OnLoaded);
        }

        public void Exit()
        {
        }

        private void OnLoaded()
        {
            _gameSceneLoaded.OnEvent?.Invoke();
        }
    }
}