namespace RogueDungeon.Combat
{
    public interface ICombatTarget
    {
        bool IsAlive { get; }
        void TakeDamage(float damage);
    }
}