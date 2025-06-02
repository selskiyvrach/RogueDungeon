namespace Game.Features.Enemies.Domain
{
    public interface IEnemySpawner
    {
        void Spawn(EnemyConfig config, EnemyPosition position);
    }
}