using Game.Features.Inventory.Domain;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public struct ProjectionData
    {
        public ItemPlacementProspect PlacementInquiry { get; }
        public Vector3 WorldPosition { get; }

        public ProjectionData(ItemPlacementProspect placementInquiry, Vector3 worldPosition)
        {
            PlacementInquiry = placementInquiry;
            WorldPosition = worldPosition;
        }
    }
}