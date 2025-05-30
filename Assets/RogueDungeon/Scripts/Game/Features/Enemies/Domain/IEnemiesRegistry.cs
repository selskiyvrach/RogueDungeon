using System.Collections.Generic;

namespace Game.Features.Enemies.Domain
{
    public interface IEnemiesRegistry
    {
        List<Enemy> Enemies { get; }
        void RegisterEnemy(Enemy enemy);
        void UnregisterEnemy(Enemy enemy);
    }
}