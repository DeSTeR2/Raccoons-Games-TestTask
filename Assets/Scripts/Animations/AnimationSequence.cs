using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Animations
{
    public class AnimationSequence : MonoBehaviour
    {
        [SerializeField] private List<Animations> controllers;

        private Action Callback;

        public async void StartAnimation()
        {
            var isThereInfLoop = false;
            var animationEnded = 0;

            SetObjectsToStartValues();
            for (var i = 0; i < controllers.Count; i++)
            {
                var anim = controllers[i];
                var animationDelay = anim.delay;

                if (anim.animation.IsThereInfiniteLoop())
                    isThereInfLoop = true;

                await Task.Delay((int)(animationDelay * 1000f));
                anim.animation.StartAnimation().Callback(() => animationEnded++);
            }

            if (isThereInfLoop) return;

            while (animationEnded < controllers.Count)
                await Task.Yield();

            InvokeCallback();
        }


        public async void StartReverceAnimation()
        {
            var isThereInfLoop = false;
            var animationEnded = 0;

            for (var i = controllers.Count - 1; i >= 0; i--)
            {
                var anim = controllers[i];
                var animationDelay = anim.delay;

                if (anim.animation.IsThereInfiniteLoop())
                    isThereInfLoop = true;

                await Task.Delay((int)(animationDelay * 1000f));
                anim.animation.StartReverceAnimation().Callback(() => animationEnded++);
            }

            if (isThereInfLoop) return;

            while (animationEnded < controllers.Count)
                await Task.Yield();

            InvokeCallback();
        }


        public void SetObjectsToStartValues()
        {
            foreach (var controller in controllers) controller.animation?.SetToStartValue();
        }

        public void SetCallback(Action callback)
        {
            Callback = callback;
        }

        private void InvokeCallback()
        {
            Callback?.Invoke();
            Callback = null;
        }
    }

    [Serializable]
    public struct Animations
    {
        public AnimationController animation;
        public float delay;
    }
}