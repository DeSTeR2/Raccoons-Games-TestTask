using System;
using System.Collections.Generic;
using Infrastructure.Event;
using Infrastructure.StateMachine.States;
using Systems.File;

namespace Infrastructure.StateMachine
{
    public class GameStateMachine : StateMachine
    {
        private readonly AllEvents _allEvents;
        private readonly SaveLoadClassSystem _saveLoadClassSystem;
        private readonly SceneLoader _sceneLoader;
        private LoadingCurtain _curtain;

        public Action OnSceneChanged;

        public GameStateMachine(SaveLoadClassSystem saveLoadClassSystem, SceneLoader sceneLoader, AllEvents allEvents)
        {
            _saveLoadClassSystem = saveLoadClassSystem;
            _sceneLoader = sceneLoader;
            _allEvents = allEvents;

            RegisterStates();
        }

        protected override void RegisterStates()
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BoostrapState)] = new BoostrapState(this, _saveLoadClassSystem),
                [typeof(LoadingState)] = new LoadingState(_sceneLoader),
                [typeof(MenuState)] = new MenuState(this, _allEvents),
                [typeof(GameState)] = new GameState(this, _allEvents)
            };
        }
    }
}