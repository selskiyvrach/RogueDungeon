using RogueDungeon.Characters;

namespace RogueDungeon.Weapons
{
    public interface IAttackDamageModifier
    {
        void ApplyModifiers(ref float damage, IDamageable target);
    }
}