using Game.Features.Inventory.Domain;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public struct ProjectionData
    {
        public ItemPlacementProspect PlacementProspect { get; }
        public Vector3 WorldPosition { get; }

        public ProjectionData(ItemPlacementProspect placementProspect, Vector3 worldPosition)
        {
            PlacementProspect = placementProspect;
            WorldPosition = worldPosition;
        }
    }
}