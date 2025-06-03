using Game.Features.Enemies.Domain;
using UnityEngine;

namespace Game.Features.Enemies.Factory
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