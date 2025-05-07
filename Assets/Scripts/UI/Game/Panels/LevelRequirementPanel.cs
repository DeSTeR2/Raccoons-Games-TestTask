using TMPro;
using UnityEngine;

namespace UI.Game.Panels
{
    public class LevelRequirementPanel : Panel
    {
        [SerializeField] private TextMeshProUGUI scoreRequirement;

        public void Open(int requiredScore)
        {
            scoreRequirement.text = requiredScore.ToString();
            Open();
        }
    }
}