using Infrastructure.StateMachine.States;

namespace Game.Box.BoxStates
{
    public class FlyState : IState
    {
        private readonly Box _box;
        private readonly BoxStateMachine _boxStateMachine;

        public FlyState(BoxStateMachine boxStateMachine, Box box)
        {
            _boxStateMachine = boxStateMachine;
            _box = box;
        }

        public void Enter()
        {
            _box.Physics(true);
        }

        public void Exit()
        {
        }
    }
}