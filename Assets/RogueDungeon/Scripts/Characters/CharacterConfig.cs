using System.Linq;
using RogueDungeon.Actions;
using RogueDungeon.Data.Stats;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public abstract class CharacterConfig : ScriptableObject, IStatsProvider
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public StatConfig[] Stats { get; private set; }

        public float GetStat(string id) =>
            Stats.FirstOrDefault(n => n.Id == id)?.GetValue() ?? 0;

        public abstract CharacterController CreateController(Character character, ActionFactory actionFactory);
    }
}