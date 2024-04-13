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
        [field: Header("Stats"), SerializeField] public StatConfig[] Stats { get; private set; }
        [Header("Actions"), SerializeField] public ActionConfig[] ActionConfigs;
        [field: Header("Unarmed"), SerializeField] public BlockingWeaponConfig UnarmedBlock { get; private set; }
        [field: SerializeField] public AttackWeaponConfig UnarmedAttack { get; private set; }
        [field: Header("SidedAttacks"), SerializeField] public AttackWeaponConfig AttackCenter { get; private set; }
        [field: SerializeField] public AttackWeaponConfig AttackLeft { get; private set; }
        [field: SerializeField] public AttackWeaponConfig AttackRight { get; private set; }


        public float GetStat(string id) =>
            Stats.FirstOrDefault(n => n.Id == id).Value;

        public ActionConfig GetActionConfig(string key) =>
            ActionConfigs.FirstOrDefault(n => n.Name == key);
    }
}