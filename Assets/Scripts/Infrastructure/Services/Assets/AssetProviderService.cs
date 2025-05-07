using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Assets
{
    public class AssetProviderService : IAssetProviderService
    {
        public TAsset GetAsset<TAsset>(string path) where TAsset : Object
        {
            return Resources.Load<TAsset>(path);
        }

        public T Instantiate<T>(string path, DiContainer container)
        {
            var go = GetAsset<GameObject>(path);
            go = container.InstantiatePrefab(go);

            var obj = go.GetComponent<T>();
            return obj;
        }

        public T Instantiate<T>(string path, Transform at, DiContainer container)
        {
            var go = GetAsset<GameObject>(path);
            go = container.InstantiatePrefab(go, at);

            var obj = go.GetComponent<T>();
            return obj;
        }
    }
}