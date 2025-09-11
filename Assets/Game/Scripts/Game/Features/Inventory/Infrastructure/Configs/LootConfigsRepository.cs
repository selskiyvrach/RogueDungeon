using System;
using System.Linq;
using Game.Features.Inventory.Domain;
using UnityEngine;

namespace Game.Features.Inventory.Infrastructure.Configs
{
    public class LootConfigsRepository : ScriptableObject, ILootConfigsRepository
    {
        [Serializable]
        private class LootConfig : ILootConfig
        {
            [field: SerializeField] public string Id;
            [field: SerializeField] public string[] Items { get; private set; }
        }

        [SerializeField] private LootConfig[] _configs;
    
        public ILootConfig GetLootConfig(string lootId) => 
            _configs.First(n => n.Id == lootId);
    }
}