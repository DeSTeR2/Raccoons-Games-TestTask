using Systems.File;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace Infrastructure.StateMachine.States
{
    public class BoostrapState : IPayloadState<int>
    {
        private readonly SaveLoadClassSystem _saveLoadClassSystem;
        private readonly GameStateMachine _stateMachine;

        public BoostrapState(GameStateMachine stateMachine, SaveLoadClassSystem saveLoadClassSystem)
        {
            _stateMachine = stateMachine;
            _saveLoadClassSystem = saveLoadClassSystem;
        }

        public void Enter(int param)
        {
            Screen.orientation = ScreenOrientation.Portrait;
            _saveLoadClassSystem.FindAndLoad();
            _stateMachine.Enter<MenuState>();
        }

        public void Exit()
        {
        }
    }
}