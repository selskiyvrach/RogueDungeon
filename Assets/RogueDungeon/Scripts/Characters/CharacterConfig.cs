using RogueDungeon.Actions;
using RogueDungeon.Data.Stats;
using UnityEngine;

namespace RogueDungeon.Characters
{
    public abstract class CharacterConfig : ScriptableObject, IStatsProvider
    {
        [SerializeField] private string _id;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private StatsConfig _stats;
        [SerializeField] private ActionConfig _staggerActionConfig;
        [SerializeField] private ActionConfig _deathActionConfig;
        public ActionConfig StaggerActionConfig => _staggerActionConfig;
        public ActionConfig DeathActionConfig => _deathActionConfig;
        public string Id => _id;
        public GameObject Prefab => _prefab;
        public float GetStat(string id) =>
            _stats.GetStat(id);

        public abstract CharacterController CreateController(Character character);
    }
}