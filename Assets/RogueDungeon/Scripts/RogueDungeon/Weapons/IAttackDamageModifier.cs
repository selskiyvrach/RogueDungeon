using RogueDungeon.Characters;

namespace RogueDungeon.Weapons
{
    public interface IAttackDamageModifier
    {
        void ApplyModifiers(ref float damage, IDamageable target);
    }

    public class DummyAttackDamageModifier : IAttackDamageModifier
    {
        public void ApplyModifiers(ref float damage, IDamageable target)
        {
            damage += 2;
        }
    }
}