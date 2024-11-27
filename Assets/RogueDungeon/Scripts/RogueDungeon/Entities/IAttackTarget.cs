using RogueDungeon.Collisions;

namespace RogueDungeon.Entities
{
    public interface IAttackTarget
    {
        void TakeDamage(float damage);
    }
}