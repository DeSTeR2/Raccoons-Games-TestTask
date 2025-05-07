using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Animations;
using Infrastructure.Event;
using Infrastructure.Events;
using UnityEngine;

namespace UI.Menu
{
    public class MenuAnimationController : IDisposable
    {
        private const int DelayToShowAnimation = 500;
        private readonly Transform _blocksParent;
        private readonly AnimationSequence _buttonsAnimation;
        private readonly EventSO gameSceneLoaded;

        private readonly EventSO menuSceneLoaded;

        private List<AnimationController> _blockAnimations;

        public MenuAnimationController(Transform blocksParent, AnimationSequence buttonsAnimation, AllEvents allEvents)
        {
            _blocksParent = blocksParent;
            _buttonsAnimation = buttonsAnimation;
            _buttonsAnimation.SetObjectsToStartValues();

            menuSceneLoaded = allEvents.Find<MenuEvents>().menuSceneLoaded;
            gameSceneLoaded = allEvents.Find<LevelEvent>().gameSceneLoaded;

            Subscribe();
            FindBlockAnimations();
        }

        public void Dispose()
        {
            menuSceneLoaded.OnEvent -= PlayAnimations;
            gameSceneLoaded.OnEvent -= Dispose;
        }

        private void Subscribe()
        {
            menuSceneLoaded.OnEvent += PlayAnimations;
            gameSceneLoaded.OnEvent += Dispose;
        }

        private void FindBlockAnimations()
        {
            _blockAnimations = new List<AnimationController>();
            for (var i = 0; i < _blocksParent.childCount; i++)
            {
                var child = _blocksParent.GetChild(i);
                var blockImage = child.GetChild(0);
                var animation = blockImage.GetComponent<AnimationController>();

                _blockAnimations.Add(animation);
            }
        }

        private async void PlayAnimations()
        {
            await Task.Delay(DelayToShowAnimation);
            foreach (var controller in _blockAnimations) controller?.StartAnimation();

            _buttonsAnimation?.StartAnimation();
        }
    }
}