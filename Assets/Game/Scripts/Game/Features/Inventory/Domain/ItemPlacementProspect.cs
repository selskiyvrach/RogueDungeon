using Game.Libs.Items;
using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public record ItemPlacementProspect(bool IsPossible, Vector2 PosNormalized, IItem ReplacedItem)
    {
        public bool IsPossible { get; } = IsPossible;
        public Vector2 PosNormalized { get; } = PosNormalized;
        public IItem ReplacedItem { get; } = ReplacedItem;
    }
}