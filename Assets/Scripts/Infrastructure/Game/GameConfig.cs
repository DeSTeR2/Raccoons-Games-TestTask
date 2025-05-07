using Game.Box;
using UnityEngine;

namespace Infrastructure.Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Config")]
    public class GameConfig : ScriptableObject
    {
        public float MergeImpulseThreshold = 5;
        public float WaitToSpawnNewBox = 0.5f;
        public Range ScoreRequirements;
    }
}