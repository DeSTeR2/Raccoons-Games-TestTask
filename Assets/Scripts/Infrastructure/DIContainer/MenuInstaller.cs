using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer
{
    public class MenuInstaller : MonoInstaller
    {
        [SerializeField] GameObject menu;

        public override void InstallBindings()
        {
            Container.InstantiatePrefab(menu);
        }
    }
}