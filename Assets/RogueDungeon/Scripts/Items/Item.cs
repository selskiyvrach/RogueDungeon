using System;
using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Stats;
using UnityEngine;

namespace RogueDungeon.Items
{
    [Serializable]
    public class AttackWeaponConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public string DamageType { get; private set; }
        [field: SerializeField] public ActionConfig AttackActionConfig { get; private set; }
    }

    [Serializable]
    public class BlockingWeaponConfig : IStatsProvider
    {
        [field: SerializeField] public StatConfig[] Stats { get; private set; }
        [field: SerializeField] public ActionConfig BlockActionConfig { get; private set; }
        
        public float GetStat(string id) => 
            Stats.FirstOrDefault(n => n.Id == id).Value;
    }
}