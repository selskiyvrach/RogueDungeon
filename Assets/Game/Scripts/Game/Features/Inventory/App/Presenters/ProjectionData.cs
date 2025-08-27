using Game.Features.Inventory.Domain;
using UnityEngine;

namespace Game.Features.Inventory.App.Presenters
{
    public struct ProjectionData
    {
        public ItemPlacementInquiryResult PlacementInquiry { get; }
        public Vector3 WorldPosition { get; }

        public ProjectionData(ItemPlacementInquiryResult placementInquiry, Vector3 worldPosition)
        {
            PlacementInquiry = placementInquiry;
            WorldPosition = worldPosition;
        }
    }
}