using System;
using Game.Score;
using Infrastructure.Event;
using Infrastructure.Events;
using Infrastructure.Game;
using Infrastructure.StateMachine;
using Infrastructure.StateMachine.States;
using UI.Game.Panels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private LevelRequirementPanel requirementPanel;
        [SerializeField] private WinPanel winPanel;
        [SerializeField] private Button homeBtn;
        private GameConfig _gameConfig;
        private ScoreContainer _scoreContainer;
        private EventSO gameSceneLoaded;

        private int requaredScore;
        private GameStateMachine _gameStateMachine;

        private void OnDestroy()
        {
            gameSceneLoaded.OnEvent -= InitPanels;
        }

        [Inject]
        public void Construct(AllEvents events, ScoreContainer scoreContainer, 
            GameConfig gameConfig, GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _gameConfig = gameConfig;
            _scoreContainer = scoreContainer;
            _scoreContainer.OnScoreChanged += CheckWin;

            gameSceneLoaded = events.Find<LevelEvent>().gameSceneLoaded;
            gameSceneLoaded.OnEvent += InitPanels;
        }

        private void Start()
        {
            homeBtn.onClick.AddListener(() =>
            {
                _gameStateMachine.Enter<MenuState>();
            });
        }

        private void CheckWin()
        {
            if (_scoreContainer.Score >= requaredScore) winPanel.Open();
        }

        private void InitPanels()
        {
            requaredScore = (int)_gameConfig.ScoreRequirements.RandomNumber;
            requirementPanel.Open(requaredScore);
        }
    }
}