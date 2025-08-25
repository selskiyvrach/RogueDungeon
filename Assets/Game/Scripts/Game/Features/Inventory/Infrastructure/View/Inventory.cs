using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField, HideInInspector] private Container[] _containers;

        private void OnValidate() => 
            _containers = GetComponentsInChildren<Container>(true);

        [Inject]
        public void Construct(Camera gameCamera) => 
            _inventoryCanvas.worldCamera = gameCamera;
    }

    public class DraggableArea : MonoBehaviour
    {
        private RectTransform _rectTransform;
        private Camera _camera;

        private void OnValidate() => 
            _rectTransform = GetComponent<RectTransform>();

        [Inject]
        public void Construct(Camera gameCamera) => 
            _camera = gameCamera;

        public Vector3 ScreenToWorldPoint(Vector2 screenPoint)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(_rectTransform, screenPoint, _camera, out var point);
            return point;
        }
    }

    public class ItemDragger : MonoBehaviour
    {
        private DraggableArea _draggableArea;
        private ICursor _cursor;
        private IItemView _item;

        [Inject]
        public void Construct(ICursor cursor, DraggableArea draggableArea)
        {
            _cursor = cursor;
            _draggableArea = draggableArea;
        }

        private void Update() =>
            transform.position = _draggableArea.ScreenToWorldPoint(_cursor.ScreenPos);

        public void StartDrag(IItemView item)
        {
            _item = item;
            _item.SetParent(transform);
            _item.SetLocalPosition(Vector3.zero);
        }

        public void ReleaseDrag() => 
            _item.SetParent(null);
    }
}