using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.Game
{
    public class BootsTrapper : MonoBehaviour, ICoroutineRunner
    {
        private GameStateMachine _gameStateMachine;

        [Inject]
        public void Construct(GameStateMachine GameStateMachine)
        {
            _gameStateMachine = GameStateMachine;
        }
        
        private void Awake()
        {
            int currentScene = SceneManager.GetActiveScene().buildIndex;

            _gameStateMachine.Enter<BoostrapState, int>(currentScene);
            DontDestroyOnLoad(gameObject);
        }
    }
}