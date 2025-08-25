using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public sealed record FreeSpaceItemPlacement(IItem Item, Vector2 Position) : ItemPlacement(Item)
    {
        public Vector2 Position { get; } = Position;
    }
}