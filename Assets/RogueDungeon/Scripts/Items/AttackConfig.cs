using System;
using RogueDungeon.Actions;
using RogueDungeon.Characters;
using RogueDungeon.Data.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{
    [Serializable]
    public class AttackConfig : IAttackConfig
    {
        [SerializeField] private StatConfig _damage;
        [SerializeField] private StatConfig _balanceDamage;
        [field: SerializeField] public string DamageType { get; private set; }
        [field: SerializeField] public DodgeState DodgeableBy { get; private set; } = DodgeState.NotDodging;
        [SerializeField] private ActionConfig _attackActionConfig;
        
        public IActionConfig AttackActionConfig => _attackActionConfig;
        public float BalanceDamage => _balanceDamage.GetValue();
        public float Damage => _damage.GetValue();
        public AttackAction CreateAction() => 
            new(this);
    }

    [Serializable]
    public class AttackComboConfig
    {
        [field: SerializeField] public AttackConfig BaseAttack { get; private set; }
        [field: SerializeField] public string[] Animations { get; private set; }
        [field: SerializeField] public bool UseCustomFinalAttack { get; private set; }
        [field: ShowIf("UseCustomFinalAttack"), SerializeField] public AttackConfig FinalAttack { get; private set; }
    }
}