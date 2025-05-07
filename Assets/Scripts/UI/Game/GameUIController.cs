using Game.Score;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.Game
{
    public class GameUIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        private ScoreContainer _scoreContainer;

        private void Start()
        {
            UpdateUI();
        }

        [Inject]
        public void Construct(ScoreContainer scoreContainer)
        {
            _scoreContainer = scoreContainer;
            _scoreContainer.OnScoreChanged += UpdateUI;
        }

        private void UpdateUI()
        {
            scoreText.text = $"Score: {_scoreContainer.Score}";
        }
    }
}