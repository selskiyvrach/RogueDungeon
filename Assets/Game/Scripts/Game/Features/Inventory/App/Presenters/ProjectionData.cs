using Game.Features.Inventory.Domain;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public struct ProjectionData
    {
        public ItemPlacementResult Placement { get; }
        public Vector3 WorldPosition { get; }

        public ProjectionData(ItemPlacementResult placement, Vector3 worldPosition)
        {
            Placement = placement;
            WorldPosition = worldPosition;
        }
    }
}