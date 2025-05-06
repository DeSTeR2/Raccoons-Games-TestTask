using System;
using System.Collections.Generic;
using Infrastructure.StateMachine.States;
using Systems.File;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine
    {
        private readonly SaveLoadClassSystem _saveLoadClassSystem;
        private readonly SceneLoader _sceneLoader;
        private Dictionary<Type, IExitableState> _states;
        private IExitableState _currectState;
        private LoadingCurtain _curtain;
        
        public Action OnSceneChanged;

        public GameStateMachine(SaveLoadClassSystem saveLoadClassSystem, SceneLoader sceneLoader)
        {
            _saveLoadClassSystem = saveLoadClassSystem;
            _sceneLoader = sceneLoader;

            RegisterStates();
        }

        private void RegisterStates()
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BoostrapState)] = new BoostrapState(this, _saveLoadClassSystem),
                [typeof(LoadingState)] = new LoadingState(_sceneLoader),
                [typeof(MenuState)] = new MenuState(this),
                [typeof(GameState)] = new GameState(this)
            };
        }

        public void Enter<TState>() where TState : IState
        {
            _currectState?.Exit();
            _currectState = _states[typeof(TState)];
            (_currectState as IState)?.Enter();
        }

        public void Enter<TStateWithParam, TParam>(TParam param) where TStateWithParam : class, IPayloadState<TParam>
        {
            _currectState?.Exit();
            _currectState = _states[typeof(TStateWithParam)];
            (_currectState as TStateWithParam)?.Enter(param);
        }
        
        public void Enter<TStateWithParamExitNotify, TParam>(TParam param, Action onExit = null) where TStateWithParamExitNotify : class, IPayloadExitNotify<TParam>
        {
            _currectState?.Exit();
            _currectState = _states[typeof(TStateWithParamExitNotify)];
            (_currectState as TStateWithParamExitNotify)?.Enter(param, onExit);
        }
    }
}