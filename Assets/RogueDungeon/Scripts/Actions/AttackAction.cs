using RogueDungeon.Characters;
using RogueDungeon.Items;
using RogueDungeon.Stats;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Actions
{
    public class AttackAction : Action
    {
        private readonly Character _character;
        private readonly AttackWeaponConfig _weapon;
        private DodgeState _dodgeableBy;
        
        public AttackAction(Character character, AttackWeaponConfig weapon, DodgeState dodgeableBy) : base(weapon.AttackActionConfig)
        {
            _character = character;
            _weapon = weapon;
            _dodgeableBy = dodgeableBy;
        }

        protected override void OnKeyframe(string keyframe)
        {
            Assert.AreEqual(keyframe, "Hit");
            var defender = _character.CombatState.Enemy;
            if (defender == null)
            {
                Debug.Log("Attack keyframe skipped - no enemy found");
                return;
            }

            var dodged = (defender.CombatState.DodgeState & _dodgeableBy) != 0;
            if(dodged)
                return;

            var damage = _weapon.Damage;
            var damageType = _weapon.DamageType;

            while (damageType != null)
            {
                damage += _character.GetStat(damageType + Constants.BONUS + Constants.FLAT);
                damage *= (100 + _character.GetStat(damageType + Constants.BONUS + Constants.PERCENT)) / 100;
                damageType = Constants.DAMAGE_TYPE_PARENTS[damageType];
            }

            damage = GetDamageAfterReduction(defender, damage, _weapon.DamageType);
            defender.Health.TakeDamage(damage);
            Debug.Log($"{defender.Id}'s health is {defender.Health.Current}/{defender.Health.Max}");
        }

        private static float GetDamageAfterReduction(IStatsProvider defender, float damage, string damageType)
        {
            var resistFlat = defender.GetStat(damageType + Constants.RESIST + Constants.FLAT);
            var resistPercent = defender.GetStat(damageType + Constants.RESIST + Constants.PERCENT);
            
            damage *= (100 - resistPercent) / 100;
            damage -= resistFlat;
            damage = Mathf.Clamp(damage, 0, float.MaxValue);
            
            var parent = Constants.DAMAGE_TYPE_PARENTS[damageType];
            if (parent != null)
                damage = GetDamageAfterReduction(defender, damage, parent);
            
            return damage;
        }
    }
}