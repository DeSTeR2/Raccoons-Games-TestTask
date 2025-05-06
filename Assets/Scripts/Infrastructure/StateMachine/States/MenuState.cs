using Infrastructure.Game;

namespace Infrastructure.StateMachine.States
{
    public class MenuState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public MenuState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<LoadingState, string>(Scenes.MenuScene);
        }

        public void Exit() {}
    }
}