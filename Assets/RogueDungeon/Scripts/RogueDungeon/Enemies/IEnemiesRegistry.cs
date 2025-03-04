using System.Collections.Generic;

namespace RogueDungeon.Enemies.HiveMind
{
    public interface IEnemiesRegistry
    {
        IEnumerable<Enemy> Enemies { get; }
        void RegisterEnemy(Enemy enemy);
        void UnregisterEnemy(Enemy enemy);
    }
}