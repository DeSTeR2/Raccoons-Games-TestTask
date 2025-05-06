using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public class AssetProviderService : IAssetProviderService
    {
        public TAsset GetAsset<TAsset>(string path) where TAsset : Object
        {
            return Resources.Load<TAsset>(path);
        }

        public void Instantiate(string path)
        {
            GameObject go = GetAsset<GameObject>(path);
            Object.Instantiate(go);
        }

        public void Instantiate(string path, Transform at)
        {            
            GameObject go = GetAsset<GameObject>(path);
            Object.Instantiate(go, at.position, Quaternion.identity);
        }
    }
}