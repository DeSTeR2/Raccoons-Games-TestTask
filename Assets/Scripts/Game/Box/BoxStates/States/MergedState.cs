using Infrastructure.StateMachine.States;

namespace Game.Box.BoxStates
{
    public class MergedState : IState
    {
        private readonly Box _box;
        private readonly BoxStateMachine _boxStateMachine;

        public MergedState(BoxStateMachine boxStateMachine, Box box)
        {
            _boxStateMachine = boxStateMachine;
            _box = box;
        }

        public void Enter()
        {
            _box.MergeAnimation();
            SoundManager.instance.PlaySound(SoundType.BoxMerged);
        }

        public void Exit()
        {
        }
    }
}