using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Assets
{
    public static class AssetProvider
    {
        public static T Get<T>(string path) where T : Object
        {
            var item = Resources.Load<T>(path);
            Assert.IsNotNull(item, $"Resource not found by path \"{path}\"");
            return item;
        }
    }
}