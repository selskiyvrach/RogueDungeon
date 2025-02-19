using RogueDungeon.Combat;

namespace RogueDungeon.Enemies
{
    public readonly struct EnemySpawningArgs
    {
        public readonly EnemyConfig Config;
        public readonly EnemyPosition Position;

        public EnemySpawningArgs(EnemyConfig config, EnemyPosition position)
        {
            Config = config;
            Position = position;
        }
    }
}