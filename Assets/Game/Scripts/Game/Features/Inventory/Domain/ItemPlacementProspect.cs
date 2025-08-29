using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public record ItemPlacementProspect(bool IsPossible, PositionNormalized Position, IItem ReplacedItem)
    {
        public bool IsPossible { get; } = IsPossible;
        public IItem ReplacedItem { get; } = ReplacedItem;
        public PositionNormalized Position { get; } = Position;
    }
}