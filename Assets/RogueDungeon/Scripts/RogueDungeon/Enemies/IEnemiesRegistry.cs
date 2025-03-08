using System.Collections.Generic;

namespace RogueDungeon.Enemies.HiveMind
{
    public interface IEnemiesRegistry
    {
        List<Enemy> Enemies { get; }
        void RegisterEnemy(Enemy enemy);
        void UnregisterEnemy(Enemy enemy);
    }
}