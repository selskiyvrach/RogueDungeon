namespace Enemies
{
    public interface IEnemySpawner
    {
        void Spawn(EnemyConfig config, EnemyPosition position);
    }
}