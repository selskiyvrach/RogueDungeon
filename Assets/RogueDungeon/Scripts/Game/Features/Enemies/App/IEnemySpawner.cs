using Game.Features.Enemies.Domain;

namespace Game.Features.Enemies.App
{
    public interface IEnemySpawner
    {
        void Spawn(EnemyConfig config, EnemyPosition position);
    }
}