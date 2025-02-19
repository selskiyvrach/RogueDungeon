namespace RogueDungeon.Enemies
{
    public interface IEnemySpawner
    {
        void Spawn(EnemySpawningArgs args);
    }
}