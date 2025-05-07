using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Assets
{
    public interface IAssetProviderService
    {
        TAsset GetAsset<TAsset>(string path) where TAsset : Object;
        T Instantiate<T>(string path, DiContainer container);
        T Instantiate<T>(string path, Transform at, DiContainer container);
    }
}