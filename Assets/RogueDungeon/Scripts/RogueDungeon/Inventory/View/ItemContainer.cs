using System.Collections.Generic;
using Common.UtilsDotNet;
using Inventory.Shared;
using UnityEngine;

namespace Inventory.View
{
    public abstract class ItemContainer : MonoBehaviour
    {
        protected readonly List<InventoryItemView> Items = new();
        protected RectTransform _rectTransform;

        protected abstract float CellSize { get; }

        protected virtual void OnValidate() => 
            _rectTransform = GetComponent<RectTransform>().ThrowIfNull();

        public bool TryRaycastItem(Vector3 screenPos, UnityEngine.Camera camera, out InventoryItemView inventoryItem, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            inventoryItem = null;
            return RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPos, camera) && 
                   (inventoryItem = RaycastItem(screenPos, out extractItemCommand)) != null;
        }
        
        public bool TryProjectItem(InventoryItemView inventoryItem, Vector3 screenPos, UnityEngine.Camera camera, out ICommand placeItemCommand)
        {
            placeItemCommand = null;
            
            if(!RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPos, camera)) 
                return false;

            // cell size before projections since it affects grid projection
            inventoryItem.SetCellSize(CellSize);
            GetItemProjection(inventoryItem, screenPos, out var projectionPos, out var isPositionLegal, out placeItemCommand);
            inventoryItem.SetupProjection(projectionPos, isPositionLegal);
            return true;
        }

        protected abstract InventoryItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand);
        protected abstract void GetItemProjection(InventoryItemView inventoryItem, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand);
    }
}