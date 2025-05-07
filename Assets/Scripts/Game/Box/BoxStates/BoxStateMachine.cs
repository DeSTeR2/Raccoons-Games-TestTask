using System;
using System.Collections.Generic;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;

namespace Game.Box.BoxStates
{
    public class BoxStateMachine : StateMachine
    {
        private readonly Box _box;

        public BoxStateMachine(Box box)
        {
            _box = box;
            RegisterStates();
        }

        protected override void RegisterStates()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(ShootingState)] = new ShootingState(this, _box),
                [typeof(FlyState)] = new FlyState(this, _box),
                [typeof(MergedState)] = new MergedState(this, _box)
            };
        }
    }
}