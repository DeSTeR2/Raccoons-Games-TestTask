using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProviderService
    {
        TAsset GetAsset<TAsset>(string path) where TAsset : Object;
        void Instantiate(string path);
        void Instantiate(string path, Transform at);
    }
}