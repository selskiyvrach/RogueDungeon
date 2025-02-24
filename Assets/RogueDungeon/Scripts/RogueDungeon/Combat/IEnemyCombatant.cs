namespace RogueDungeon.Combat
{
    public interface IEnemyCombatant : ICombatTarget
    {
        EnemyPosition CombatPosition { get; }
    }
}