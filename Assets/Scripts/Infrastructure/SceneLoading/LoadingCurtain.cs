using System.Collections;
using Animations;
using TMPro;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private AnimationController loadingAnimation;
        [SerializeField] private AnimationController curtainDisappearAnimation;
        [SerializeField] private CanvasGroup canvasGroup;

        [Space] [SerializeField] private TextMeshProUGUI loadingText;

        [SerializeField] private float timeToChangeText = 0.3f;
        private bool _isRunning;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            loadingAnimation.StartAnimation();
        }

        public void StartLoading()
        {
            canvasGroup.alpha = 1;
            gameObject.SetActive(true);
            _isRunning = true;
            StartCoroutine(TextAnimation());
        }

        public void StopLoading()
        {
            StopCoroutine(TextAnimation());
            curtainDisappearAnimation.StartAnimation().Callback(CurtainDisappear);
        }

        private void CurtainDisappear()
        {
            _isRunning = false;
            gameObject.SetActive(false);
        }

        private IEnumerator TextAnimation()
        {
            var dots = 0;
            while (_isRunning)
            {
                loadingText.text = "Loading";
                for (var i = 0; i < dots; i++) loadingText.text += ".";

                dots++;
                dots %= 4;

                yield return new WaitForSeconds(timeToChangeText);
            }
        }
    }
}