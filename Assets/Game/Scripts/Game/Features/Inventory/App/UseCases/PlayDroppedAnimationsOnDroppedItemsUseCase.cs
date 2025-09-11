using System;
using System.Collections.Generic;
using System.Linq;
using Game.Features.Inventory.App.Presenters;
using Game.Features.Inventory.Domain;
using Game.Libs.Items;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.App.UseCases
{
    public class PlayDroppedAnimationsOnDroppedItemsUseCase : IInitializable, IDisposable
    {
        private readonly ILootDropper _lootDropper;
        private readonly IPresentersRegistry _presentersRegistry;

        public PlayDroppedAnimationsOnDroppedItemsUseCase(ILootDropper lootDropper, IPresentersRegistry presentersRegistry)
        {
            _lootDropper = lootDropper;
            _presentersRegistry = presentersRegistry;
        }

        public void Initialize() => 
            _lootDropper.OnItemsDropped += PlayDropAnimations;

        public void Dispose() => 
            _lootDropper.OnItemsDropped -= PlayDropAnimations;

        private void PlayDropAnimations(Vector2Int arg1, List<IItem> arg2)
        {
            foreach (var item in arg2) 
                _presentersRegistry.Items.First(n => n.Model == item).PlayDroppedAnimation();
        }
    }
}