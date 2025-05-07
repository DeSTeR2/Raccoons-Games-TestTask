using System;
using UnityEngine;

namespace Game.Score
{
    [CreateAssetMenu(fileName = "Score Container", menuName = "Score/Container")]
    public class ScoreContainer : ScriptableObject
    {
        private int _score;

        public Action OnScoreChanged;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnScoreChanged?.Invoke();
            }
        }
    }
}