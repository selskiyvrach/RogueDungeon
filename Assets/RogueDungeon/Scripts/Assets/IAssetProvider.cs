using UnityEngine;

namespace RogueDungeon.Assets
{
    public interface IAssetProvider
    {
        T GetAsset<T>(string path) where T : Object;
        T GetAssetInstance<T>(string path, Transform parent = null) where T : Object;
    }
}