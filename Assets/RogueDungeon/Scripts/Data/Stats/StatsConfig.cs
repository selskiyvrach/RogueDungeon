using System;
using System.Linq;
using UnityEngine;

namespace RogueDungeon.Data.Stats
{
    [Serializable]
    public class StatsConfig 
    {
        [Serializable]
        private class StatById
        {
            public string Id;
            public StatConfig StatConfig;
        }
        
        [SerializeField] private StatById[] _stats;

        public float GetStat(string id) => 
            _stats.FirstOrDefault(n => n.Id == id)?.StatConfig.GetValue() ?? 0;
    }
}