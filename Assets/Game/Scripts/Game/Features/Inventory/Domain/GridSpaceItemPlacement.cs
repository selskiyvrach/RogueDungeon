using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public sealed record GridSpaceItemPlacement(IItem Item, Vector2Int Position) : ItemPlacement(Item)
    {
        public Vector2Int Position { get; } = Position;
    }
}