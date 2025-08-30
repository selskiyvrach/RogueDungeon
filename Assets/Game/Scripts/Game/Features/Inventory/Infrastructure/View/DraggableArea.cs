using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class DraggableArea : MonoBehaviour, IDraggableArea
    {
        [SerializeField] private RectTransform _rectTransform;
        [field: SerializeField] public float CellSize { get; private set; } = 25f;
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
}