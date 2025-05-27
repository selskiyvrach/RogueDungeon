using System.Collections.Generic;

namespace Enemies
{
    public interface IEnemiesRegistry
    {
        List<Enemy> Enemies { get; }
        void RegisterEnemy(Enemy enemy);
        void UnregisterEnemy(Enemy enemy);
    }
}