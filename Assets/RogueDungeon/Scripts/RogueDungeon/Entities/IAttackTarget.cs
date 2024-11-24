namespace RogueDungeon.Entities
{
    public interface IAttackTarget : IRootEntity
    {
        void TakeDamage(float damage);
    }
}