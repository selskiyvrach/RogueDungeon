using System;
using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Characters;
using RogueDungeon.Stats;
using UnityEngine;

namespace RogueDungeon.Items
{
    [Serializable]
    public class AttackConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public string DamageType { get; private set; }
        [field: SerializeField] public DodgeState DodgeableBy { get; private set; } = DodgeState.NotDodging;
        [field: SerializeField] public ActionConfig AttackActionConfig { get; private set; }

        public AttackAction CreateAction() => 
            new(this);
    }

    [Serializable]
    public class BlockConfig : IStatsProvider
    {
        [field: SerializeField] public StatConfig[] Stats { get; private set; }
        [field: SerializeField] public ActionConfig BlockActionConfig { get; private set; }
        
        public float GetStat(string id) => 
            Stats.FirstOrDefault(n => n.Id == id).Value;
    }
}