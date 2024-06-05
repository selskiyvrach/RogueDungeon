using RogueDungeon.Characters;
using RogueDungeon.Data.Stats;
using RogueDungeon.Items;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Actions
{
    public class AttackAction : Action
    {
        private readonly IAttackConfig _attackConfig;
        
        public AttackAction(IAttackConfig attackConfig) : base(attackConfig.AttackActionConfig) => 
            _attackConfig = attackConfig;

        protected override void OnKeyframe(string keyframe)
        {
            Assert.AreEqual(keyframe, "Hit");

            var defender = Character.CombatState.SurroundingCharacters.GetTargetForPosition(Character.CombatState.Position);
            if (defender == null)
                return;

            var dodged = defender.CombatState.DodgeState != DodgeState.NotDodging && (defender.CombatState.DodgeState & _attackConfig.DodgeableBy) != 0;
            if(dodged)
                return;

            // middle attack is blocked fully, side attack deal reduced damage
            var isBlocking = defender.CombatState.BlockIsRaised;
            if(isBlocking && _attackConfig.DodgeableBy == DodgeState.NotDodging)
                return;

            var damage = _attackConfig.Damage;
            var damageType = _attackConfig.DamageType;

            while (damageType != null)
            {
                damage += Character.GetStat(damageType + Constants.BONUS + Constants.FLAT);
                damage *= (100 + Character.GetStat(damageType + Constants.BONUS + Constants.PERCENT)) / 100;
                damageType = Constants.DAMAGE_TYPE_PARENTS[damageType];
            }

            damage = GetDamageAfterReduction(defender, damage, _attackConfig.DamageType, isBlocking);
            
            defender.TakeDamage(damage, _attackConfig.BalanceDamage);
        }

        private static float GetDamageAfterReduction(Character defender, float damage, string damageType, bool isBlocking)
        {
            var resistFlatName = damageType + Constants.RESIST + Constants.FLAT;
            var resistFlat = defender.GetStat(resistFlatName);
            if (isBlocking)
                resistFlat += defender.CombatState.BlockingWeaponStats.GetStat(resistFlatName);
            
            var resistPercentName = damageType + Constants.RESIST + Constants.PERCENT;
            var resistPercent = defender.GetStat(resistPercentName);
            if (isBlocking)
                resistPercent += defender.CombatState.BlockingWeaponStats.GetStat(resistPercentName);
            
            damage *= 1 - resistPercent;
            damage -= resistFlat;
            damage = Mathf.Clamp(damage, 0, float.MaxValue);
            
            var parent = Constants.DAMAGE_TYPE_PARENTS[damageType];
            if (parent != null)
                damage = GetDamageAfterReduction(defender, damage, parent, isBlocking);
            
            return damage;
        }
    }
}