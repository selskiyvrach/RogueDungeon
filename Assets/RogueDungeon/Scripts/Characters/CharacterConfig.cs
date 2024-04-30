using System.Linq;
using RogueDungeon.Stats;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public abstract class CharacterConfig : ScriptableObject, IStatsProvider
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public StatConfig[] Stats { get; private set; }

        public float GetStat(string id) =>
            Stats.FirstOrDefault(n => n.Id == id).Value;

        public abstract CharacterActionsController CreateController(Character character);
    }
}