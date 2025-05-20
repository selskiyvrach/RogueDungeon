using System.Collections.Generic;
using Common.UtilsDotNet;
using UnityEngine;
using UnityEngine.Assertions;

namespace RogueDungeon.Player.Model.Inventory
{
    public abstract class ItemContainer : MonoBehaviour
    {
        protected readonly List<ItemView> Items = new();
        protected RectTransform _rectTransform;

        protected abstract float CellSize { get; }

        protected virtual void OnValidate() => 
            _rectTransform = GetComponent<RectTransform>().ThrowIfNull();

        public bool TryRaycastItem(Vector3 screenPos, UnityEngine.Camera camera, out ItemView item, out ICommand extractItemCommand)
        {
            extractItemCommand = null;
            item = null;
            return RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPos, camera) && 
                   (item = RaycastItem(screenPos, out extractItemCommand)) != null;
        }
        
        public bool TryProjectItem(ItemView item, Vector3 screenPos, UnityEngine.Camera camera, out ICommand placeItemCommand)
        {
            placeItemCommand = null;
            
            if(!RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPos, camera)) 
                return false;

            // cell size before projections since it affects grid projection
            item.SetCellSize(CellSize);
            GetItemProjection(item, screenPos, out var projectionPos, out var isPositionLegal, out placeItemCommand);
            item.SetupProjection(projectionPos, isPositionLegal);
            return true;
        }

        protected abstract ItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand);
        protected abstract void GetItemProjection(ItemView item, Vector3 screenPos, out Vector3 projectionPos, out bool isPositionLegal, out ICommand placeItemCommand);

    }
}