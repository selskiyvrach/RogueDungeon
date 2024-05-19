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

            var damage = _attackConfig.Damage;
            var damageType = _attackConfig.DamageType;

            while (damageType != null)
            {
                damage += Character.GetStat(damageType + Constants.BONUS + Constants.FLAT);
                damage *= (100 + Character.GetStat(damageType + Constants.BONUS + Constants.PERCENT)) / 100;
                damageType = Constants.DAMAGE_TYPE_PARENTS[damageType];
            }

            damage = GetDamageAfterReduction(defender, damage, _attackConfig.DamageType);
            
            defender.TakeDamage(damage, _attackConfig.BalanceDamage);
        }

        private static float GetDamageAfterReduction(IStatsProvider defender, float damage, string damageType)
        {
            var resistFlat = defender.GetStat(damageType + Constants.RESIST + Constants.FLAT);
            var resistPercent = defender.GetStat(damageType + Constants.RESIST + Constants.PERCENT);
            
            damage *= 1 - resistPercent;
            damage -= resistFlat;
            damage = Mathf.Clamp(damage, 0, float.MaxValue);
            
            var parent = Constants.DAMAGE_TYPE_PARENTS[damageType];
            if (parent != null)
                damage = GetDamageAfterReduction(defender, damage, parent);
            
            return damage;
        }
    }
}