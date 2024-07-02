using UnityEngine;

namespace RogueDungeon.Assets
{
    public static class AssetProvider
    {
        public static T Get<T>(string path) where T : Object => 
            Resources.Load<T>(path);
    }
}