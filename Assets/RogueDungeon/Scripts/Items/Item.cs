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
        
    [Serializable]
    public class AttackConfig
    {
        [field: SerializeField] public StatConfig Damage { get; private set; }
        [field: SerializeField] public string DamageType { get; private set; }
        [field: SerializeField] public DodgeState DodgeableBy { get; private set; } = DodgeState.NotDodging;
        [field: SerializeField] public ActionConfig AttackActionConfig { get; private set; }

        public AttackAction CreateAction(StandardValues standardValues) => 
            new(this, standardValues);
    }

    [Serializable]
    public class BlockConfig : IStatsProvider
    {
        [field: SerializeField] public StatConfig[] Stats { get; private set; }
        [field: SerializeField] public ActionConfig BlockActionConfig { get; private set; }
        
        public float GetStat(string id) => 
            Stats.FirstOrDefault(n => n.Id == id)?.GetValue() ?? 0;
    }
}