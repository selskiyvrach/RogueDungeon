using System;
using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Characters;
using RogueDungeon.Data;
using RogueDungeon.Data.Stats;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RogueDungeon.Items
{


    // damage types
        // slash
        // pierce
        // blunt
        // 
    
    // attack action config
    // PLAYER
        // ANIMATION
            // swing ltr
            // swing rtl
            // thrust
            // fist l
            // fist r
            // block fists
            // block shield
            // block weapon r
            // block weapon lr
            // block weapon th
            // custom
            
        // WEAPON STATS
            // custom
            // 
            
        // DURATION
            // very fast
            // fast
            // medium
            // slow
            // very slow
            
    // ENEMY
        // ANIMATION
            // overhead smash
            // left hit
            // right hit

    public interface IAttackConfig
    {
        float Damage { get; }
        string DamageType { get; }
        DodgeState DodgeableBy { get; }
        IActionConfig AttackActionConfig { get; }
        float BalanceDamage { get; }
    }

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
    public class BlockConfig : IStatsProvider
    {
        [field: SerializeField] public StatsConfig Stats { get; private set; }
        [field: SerializeField] public ActionConfig BlockActionConfig { get; private set; }
        
        public float GetStat(string id) => 
            Stats.GetStat(id);
    }
}