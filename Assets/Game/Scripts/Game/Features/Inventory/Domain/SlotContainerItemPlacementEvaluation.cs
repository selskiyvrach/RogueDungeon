using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public readonly struct SlotContainerItemPlacementEvaluation
    {
        public readonly bool IsPossible;
        public readonly Vector2 ResultLocalPosNormalized;
        public readonly string ReplacedItemId;

        public SlotContainerItemPlacementEvaluation(bool isPossible, Vector2 resultLocalPosNormalized, string replacedItemId)
        {
            IsPossible = isPossible;
            ResultLocalPosNormalized = resultLocalPosNormalized;
            ReplacedItemId = replacedItemId;
        }
    }
}