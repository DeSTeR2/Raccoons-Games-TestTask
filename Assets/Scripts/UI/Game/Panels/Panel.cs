using Animations;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Game.Panels
{
    public class Panel : MonoBehaviour
    {
        [SerializeField] private AnimationSequence animation;
        [SerializeField] protected Button closeBtn;

        protected virtual void Start()
        {
            closeBtn.onClick.AddListener(Close);
        }

        public virtual void Open()
        {
            if (gameObject.activeInHierarchy) return;

            gameObject.SetActive(true);
            animation.StartAnimation();
        }

        protected virtual void Close()
        {
            animation.StartReverceAnimation();
            animation.SetCallback(() => { gameObject.SetActive(false); });
        }
    }
}