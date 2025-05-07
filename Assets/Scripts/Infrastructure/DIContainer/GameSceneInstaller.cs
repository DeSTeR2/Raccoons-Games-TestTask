using Game.Score;
using Infrastructure.Game;
using Managers;
using UnityEngine;
using Zenject;

namespace Infrastructure.DIContainer
{
    public class GameSceneInstaller : MonoInstaller
    {
        [SerializeField] private GameConfig GameConfig;
        [SerializeField] private ScoreContainer scoreContainer;
        [SerializeField] private LevelManager LevelManager;

        public override void InstallBindings()
        {
            Container.BindInstance(GameConfig);
            Container.BindInstance(scoreContainer);
            Container.BindInstance(LevelManager);
        }
    }
}