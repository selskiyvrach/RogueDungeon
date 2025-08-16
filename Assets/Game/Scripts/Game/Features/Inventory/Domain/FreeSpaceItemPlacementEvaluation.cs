using UnityEngine;

namespace Game.Features.Inventory.Domain
{
    public readonly struct FreeSpaceItemPlacementEvaluation
    {
        public readonly Vector2 ResultPos;
        public readonly bool IsPossible;

        public FreeSpaceItemPlacementEvaluation(Vector2 resultPos, bool isPossible)
        {
            ResultPos = resultPos;
            IsPossible = isPossible;
        }
    }
}