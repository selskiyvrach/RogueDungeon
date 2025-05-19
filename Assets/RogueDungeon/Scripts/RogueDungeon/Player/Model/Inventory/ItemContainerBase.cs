using Common.UtilsDotNet;
using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public abstract class ItemContainerBase : MonoBehaviour, IItemParent
    {
        private RectTransform _rectTransform;

        public Transform ParentObject => _rectTransform;
        public abstract float CellSize { get; }

        private void OnValidate() => 
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
            
            if(!RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, screenPos, camera) || 
               !RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, screenPos, camera, out Vector3 point))
                return false;

            ProjectItem(item, point, out placeItemCommand);
            return true;
        }

        protected abstract ItemView RaycastItem(Vector3 screenPos, out ICommand extractItemCommand);
        protected abstract void ProjectItem(ItemView item, Vector3 worldPoint, out ICommand placeItemCommand);
    }
}