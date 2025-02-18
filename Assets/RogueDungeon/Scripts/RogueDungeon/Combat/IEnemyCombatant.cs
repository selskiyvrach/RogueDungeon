namespace RogueDungeon.Combat
{
    public interface IEnemyCombatant : ICombatTarget
    {
        EnemyPosition Position { get; }
    }
}