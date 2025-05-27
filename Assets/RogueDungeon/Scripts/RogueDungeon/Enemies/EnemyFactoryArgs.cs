using UnityEngine;

namespace Enemies
{
    public readonly struct EnemyFactoryArgs
    {
        public readonly EnemyConfig Config;
        public readonly Transform Parent;

        public EnemyFactoryArgs(EnemyConfig config, Transform parent)
        {
            Config = config;
            Parent = parent;
        }
    }
}