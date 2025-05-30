using System.Collections.Generic;

namespace Game.Features.Combat.Domain
{
    public interface IPlayerRegistry
    {
        IPlayerCombatant Player { get; }
    }

    public interface IEnemiesRegistry
    {
        IEnumerable<IEnemyCombatant> Enemies { get; }
    }

    public interface IEnemyCombatant
    {
        EnemyPosition Position { get; }
        void TakeDamage(float weaponDamage, float weaponPoiseDamage);
    }
}