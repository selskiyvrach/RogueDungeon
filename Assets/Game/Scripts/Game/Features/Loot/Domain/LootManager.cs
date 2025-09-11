using System.Collections.Generic;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Loot.Domain
{
    public class LootManager : ILootDropper
    {
        // private readonly Dictionary<Vector2Int, IItemContainer>
        private readonly IFactory<string, IItem> _lootSpawner;
        private readonly ILootConfigsRepository _lootConfigsRepository;

        public LootManager(ILootConfigsRepository lootConfigsRepository, IFactory<string, IItem> lootSpawner)
        {
            _lootConfigsRepository = lootConfigsRepository;
            _lootSpawner = lootSpawner;
        }

        public void DropLoot(string lootId)
        {
            // it holds loot containers for each tile
            // it puts items into the containers
            // presenters which are already created fot said containers update their views creating items 
            // _lootSpawner.SpawnItems(_lootConfigsRepository.GetLootConfig(lootId).Items);
        }
    }
}