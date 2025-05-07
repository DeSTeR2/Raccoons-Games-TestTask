using Infrastructure.Event;
using Infrastructure.Events;
using Infrastructure.Game;

namespace Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly EventSO _onSceneLoaded;

        public MenuState(GameStateMachine gameStateMachine, AllEvents onSceneLoaded)
        {
            _gameStateMachine = gameStateMachine;
            _onSceneLoaded = onSceneLoaded.Find<MenuEvents>().menuSceneLoaded;
            onSceneLoaded.Find<MenuEvents>().loadPlayScene.OnEvent += LoadPlayScene;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadingState, string>(Scenes.MenuScene,
                () => { _onSceneLoaded.OnEvent?.Invoke(); });
        }

        public void Exit()
        {
        }

        private void LoadPlayScene()
        {
            _gameStateMachine.Enter<GameState>();
        }
    }
}