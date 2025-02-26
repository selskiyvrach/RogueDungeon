using Common.Behaviours;

namespace RogueDungeon.Combat
{
    public interface IEnemyCombatant : IBehaviour, ICombatTarget
    {
        EnemyPosition CombatPosition { get; }
    }
}