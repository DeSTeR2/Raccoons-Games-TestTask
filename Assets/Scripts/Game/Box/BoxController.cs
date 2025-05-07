using Game.Box.BoxStates;
using Game.Score;
using Infrastructure.DIContainer;
using Infrastructure.Game;
using Infrastructure.Services.Assets;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;
using Random = UnityEngine.Random;

namespace Game.Box
{
    public class BoxController : MonoBehaviour
    {
        private IAssetProviderService _assetProviderService;
        private BoxDataCollection _boxDataCollection;
        private BoxPositions _boxPositions;
        private GameConfig _config;
        private DiContainer _container;
        private ScoreContainer _scoreContainer;
        private ObjectPool<Box> boxObjectPool;

        private void Awake()
        {
            boxObjectPool = new ObjectPool<Box>(CreateBox);
        }

        [Inject]
        public void Construct(IAssetProviderService assetProviderService, BoxDataCollection boxDataCollection,
            BoxPositions boxPositions, GameConfig config, DiContainer container, ScoreContainer scoreContainer)
        {
            _scoreContainer = scoreContainer;
            _container = container;
            _config = config;
            _boxPositions = boxPositions;
            _boxDataCollection = boxDataCollection;
            _assetProviderService = assetProviderService;
        }

        public void BoxCollide(Box box1, Box box2, float impulse)
        {
            if (box1.CurrentData == box2.CurrentData && impulse >= _config.MergeImpulseThreshold)
            {
                box2.gameObject.SetActive(false);
                boxObjectPool.Release(box2);

                var newData = _boxDataCollection.GetNextData(box1.CurrentData);
                _scoreContainer.Score += box1.CurrentData.number / 2;

                box1.StateMachine.Enter<MergedState>();
                box1.SetBoxVisual(newData);
            }
        }

        public Box SpawnBox()
        {
            var box = boxObjectPool.Get();
            var boxData = GetBoxData();
            box.Init(this, _boxPositions.startPosition);
            box.SetBoxVisual(boxData);
            return box;
        }

        private BoxData GetBoxData()
        {
            var rng = Random.Range(0, 101);
            return _boxDataCollection.GetDataWithPercent(rng);
        }

        private Box CreateBox()
        {
            var box = _assetProviderService.Instantiate<Box>(Constants.BoxPath, _boxPositions.parent, _container);
            return box;
        }
    }
}