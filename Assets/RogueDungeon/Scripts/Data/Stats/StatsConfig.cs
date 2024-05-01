using System.Linq;
using UnityEngine;

namespace RogueDungeon.Data.Stats
{
    [CreateAssetMenu(menuName = "Configs/Stats", fileName = "Stats", order = 0)]
    public class StatsConfig : ScriptableObject, IStatsProvider
    {
        [SerializeField] private StatConfig[] _stats;

        public float GetStat(string id) => 
            _stats.FirstOrDefault(n => n.Id == id).Value;
    }
}