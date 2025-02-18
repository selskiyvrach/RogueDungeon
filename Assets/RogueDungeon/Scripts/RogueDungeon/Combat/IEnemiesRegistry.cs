using System.Collections.Generic;

namespace RogueDungeon.Combat
{
    public interface IEnemiesRegistry
    {
        IEnumerable<IEnemyCombatant> Enemies { get; }
        void RegisterEnemy(IEnemyCombatant enemy);
        void UnregisterEnemy(IEnemyCombatant enemy);
    }
}