using System;
using System.Threading.Tasks;
using Animations.Types;
using UnityEngine;

namespace Animations
{
    public class AnimationController : MonoBehaviour
    {
        public delegate void CallBack();

        [SerializeField] private AnimationObject[] _animations;
        [SerializeField] private bool playOnAwake;
        private int _animationEnd;

        private CallBack _callBack;

        public Action OnAnimationsEnd;

        private void Start()
        {
            for (var i = 0; i < _animations.Length; i++) _animations[i].OnAnimationEnd += ControllAnimationFlow;

            if (playOnAwake) StartAnimation();
        }

        public AnimationController StartAnimation()
        {
            _animationEnd = 0;
            for (var i = 0; i < _animations.Length; ++i) _animations[i]?.Animate();

            return this;
        }

        public async Task<AnimationController> StartAnimationAsync()
        {
            await Task.Run(() =>
            {
                _animationEnd = 0;
                for (var i = 0; i < _animations.Length; ++i) _animations[i].Animate();
            });

            return this;
        }

        public AnimationController StartReverceAnimation()
        {
            _animationEnd = 0;
            for (var i = 0; i < _animations.Length; ++i) _animations[i].AnimateReverce();

            return this;
        }

        public async Task<AnimationController> StartReverceAnimationAsync()
        {
            await Task.Run(() =>
            {
                _animationEnd = 0;
                for (var i = 0; i < _animations.Length; ++i) _animations[i].Animate();
            });

            return this;
        }

        public AnimationController StartAnimation(Transform animateObject)
        {
            _animationEnd = 0;
            for (var i = 0; i < _animations.Length; ++i) _animations[i].Animate(animateObject);

            return this;
        }

        public AnimationController Callback(CallBack callBack)
        {
            _callBack = callBack;
            return this;
        }

        private void ControllAnimationFlow()
        {
            _animationEnd++;

            if (_animationEnd == _animations.Length)
            {
                _callBack?.Invoke();
                OnAnimationsEnd?.Invoke();
                _animationEnd = 0;
            }
        }

        public void SetToStartValue()
        {
            foreach (var animationObject in _animations) animationObject.SetToStartValue();
        }

        public bool IsThereInfiniteLoop()
        {
            foreach (var animation in _animations)
                if (animation.IsInfinite())
                    return true;
            return false;
        }
    }
}