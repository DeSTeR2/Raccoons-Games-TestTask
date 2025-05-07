using Animations;
using Infrastructure.Event;
using UI.Menu;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] private Transform blocksParent;
        [SerializeField] private AnimationSequence buttonAnimations;

        [Space] [SerializeField] private MenuEvents events;

        public override void InstallBindings()
        {
            Container.BindInstance(blocksParent);
            Container.BindInstance(buttonAnimations);
            Container.BindInstance(events);

            Container.Bind<MenuAnimationController>().AsTransient().NonLazy();
        }
    }
}