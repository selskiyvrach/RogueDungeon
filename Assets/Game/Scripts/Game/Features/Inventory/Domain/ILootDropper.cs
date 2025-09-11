using System;
using System.Collections.Generic;
using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public interface ILootDropper
    {
        event Action<Vector2Int, List<IItem>> OnItemsDropped;
        void DropLoot(string lootId, Vector2Int roomCoordinates);
    }
}