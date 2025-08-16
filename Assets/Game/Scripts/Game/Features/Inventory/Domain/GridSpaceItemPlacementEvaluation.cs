using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public readonly struct GridSpaceItemPlacementEvaluation
    {
        public readonly Vector2 ResultLocalPosNormalized;
        public readonly bool IsPossible;
        public readonly string ReplacedItemId;

        public GridSpaceItemPlacementEvaluation(bool isPossible, Vector2 resultLocalPosNormalized, string replacedItemId)
        {
            ResultLocalPosNormalized = resultLocalPosNormalized;
            IsPossible = isPossible;
            ReplacedItemId = replacedItemId;
        }
    }
}