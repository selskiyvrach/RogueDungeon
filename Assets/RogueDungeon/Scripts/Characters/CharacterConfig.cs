using System;
using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Items;
using RogueDungeon.Stats;
using UnityEngine;

namespace RogueDungeon.Characters
{
    [CreateAssetMenu(menuName = "Configs/CharacterConfig", fileName = "CharacterConfig", order = 0)]
    public class CharacterConfig : ScriptableObject, IStatsProvider
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public string Controller { get; private set; }
        [field: SerializeField] public StatConfig[] Stats { get; private set; }
        [field: SerializeField] public ActionConfig[] ActionConfigs;
        [field: SerializeField] public AttackWeaponConfigById[] AttackConfigs { get; private set; }
        [field: SerializeField] public BlockWeaponConfigById[] BlockConfigs { get; private set; }

        public float GetStat(string id) =>
            Stats.FirstOrDefault(n => n.Id == id).Value;
    }
    
    [Serializable]
    public struct AttackWeaponConfigById
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public AttackWeaponConfig Config { get; private set; }
    }
    
    [Serializable]
    public struct BlockWeaponConfigById
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public BlockingWeaponConfig Config { get; private set; }
    }
}