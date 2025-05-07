using Game.Box;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer
{
    public class BoxInstaller : MonoInstaller
    {
        [SerializeField] private BoxDataCollection BoxDataCollection;
        [SerializeField] private BoxConfig BoxConfig;
        [SerializeField] private BoxPositions boxPositions;
        [SerializeField] private BoxController boxController;
        [SerializeField] private BoxMover boxMover;

        public override void InstallBindings()
        {
            Container.BindInstance(BoxDataCollection);
            Container.BindInstance(boxPositions);
            Container.BindInstance(BoxConfig);
            Container.BindInstance(boxController);
            Container.BindInstance(boxMover);
        }
    }
}