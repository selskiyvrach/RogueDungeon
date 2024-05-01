using UnityEngine;

namespace RogueDungeon.Data.Stats
{
    [CreateAssetMenu(menuName = "Configs/Stats", fileName = "Stats", order = 0)]
    public class StatsConfig : ScriptableObject, IStatsProvider
    {
        [SerializeField] private StatConfig[] _stats;

        public float GetStat(string id)
        {
            foreach (var statConfig in _stats)
                if (statConfig.Id == id)
                    return statConfig.GetValue();

            return 0;
        }
    }
}