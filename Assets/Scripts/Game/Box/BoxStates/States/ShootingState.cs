using Infrastructure.StateMachine.States;

namespace Game.Box.BoxStates
{
    public class ShootingState : IState
    {
        private readonly Box _box;
        private readonly BoxStateMachine _boxStateMachine;

        public ShootingState(BoxStateMachine boxStateMachine, Box box)
        {
            _boxStateMachine = boxStateMachine;
            _box = box;
        }

        public void Enter()
        {
            _box.Physics(false);
        }

        public void Exit()
        {
        }
    }
}