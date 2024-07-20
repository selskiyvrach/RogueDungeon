using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Assets
{
    public class AssetProvider : IAssetProvider
    {
        public T GetAsset<T>(string path) where T : Object
        {
            var item = Resources.Load<T>(path);
            Assert.IsNotNull(item, $"{typeof(T).Name} has not been found by path \"{path}\"");
            return item;
        }

        public T GetAssetInstance<T>(string path, Transform parent = null) where T : Object => 
            Object.Instantiate(GetAsset<T>(path), parent);
    }
}