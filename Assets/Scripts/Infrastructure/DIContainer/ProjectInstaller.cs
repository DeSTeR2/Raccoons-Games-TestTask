using Infrastructure.Game;
using Infrastructure.Services.Assets;
using Infrastructure.StateMachine;
using Systems.File;
using UnityEngine;
using Zenject;

namespace Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private SaveLoadClassSystem _saveLoadClassSystem;
        [SerializeField] private LoadingCurtain loadingCurtain;
        [SerializeField] float forcedTimeToWait = 3;
        [SerializeField] private BootsTrapper _bootsTrapper;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_saveLoadClassSystem).AsTransient();
            Container.BindInstance(loadingCurtain).AsTransient();
            Container.BindInstance(_bootsTrapper).AsTransient();
            Container.Bind<ICoroutineRunner>().FromInstance(_bootsTrapper).AsTransient();
            
            Container.Bind<IAssetProviderService>().To<AssetProviderService>().AsSingle();
            Container.Bind<GameStateMachine>().To<GameStateMachine>().AsSingle();
            
            Container.Bind<SceneLoader>().To<SceneLoader>().AsSingle();
            Container.BindInstance(forcedTimeToWait).AsTransient().WhenInjectedInto<SceneLoader>();
        }
    }
}