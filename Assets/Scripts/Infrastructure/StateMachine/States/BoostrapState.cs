using Infrastructure.Game;
using Systems.File;

namespace Infrastructure.StateMachine.States
{
    public class BoostrapState : IPayloadState<int>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SaveLoadClassSystem _saveLoadClassSystem;

        public BoostrapState(GameStateMachine stateMachine, SaveLoadClassSystem saveLoadClassSystem)
        {
            _stateMachine = stateMachine;
            _saveLoadClassSystem = saveLoadClassSystem;
        }
        
        public void Enter(int param)
        {
            _saveLoadClassSystem.FindAndLoad();
            _stateMachine.Enter<MenuState>();
        }

        public void Exit() { }
    }
}