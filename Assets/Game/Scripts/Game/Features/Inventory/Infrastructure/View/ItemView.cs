using Game.Features.Inventory.App.Presenters;
using Game.Libs.UI;
using Libs.Utils.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class ItemView : RaycastableGraphic, IItemView
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private ItemProjection _projection;
        [SerializeField] private Canvas _canvas;

        private bool _isBeingDragged;

        private IItemViewSetupArgs _itemViewSetupArgs;
        public string Id => _itemViewSetupArgs.Id;
        public bool IsHovered { get; private set; }

        public IProjectionView ProjectionView => _projection;

        public void Setup(IItemViewSetupArgs itemViewSetupArgs)
        {
            _itemViewSetupArgs = itemViewSetupArgs;
            _itemImage.sprite = itemViewSetupArgs.Sprite;
            _projection.SetSprite(itemViewSetupArgs.Sprite);
            RefreshSubElementsPositions();
        }

        public void SetPosition(Vector3 pos) => 
            transform.position = pos;

        public Vector2 GetScreenPosition(Camera camera) => 
            camera.WorldToScreenPoint(transform.position);

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, worldPositionStays: false);
            RefreshSubElementsPositions();
        }

        public void SetCellSize(float value)
        {
            var containerSize = (Vector2)_itemViewSetupArgs.Size * value;
            ((RectTransform)transform).sizeDelta = containerSize;
            
            var spriteAspect = _itemImage.sprite.rect.AspectRatio();

            var finalSize = spriteAspect > containerSize.AspectRatio()
                ? new Vector2(containerSize.x, containerSize.x / spriteAspect) 
                : new Vector2(containerSize.y * spriteAspect, containerSize.y);
            
            _itemImage.rectTransform.sizeDelta = finalSize;
            _projection.SetSize(finalSize);
        }

        public void DisplayHovered(bool hovered)
        {
            IsHovered = hovered;
            _canvas.overrideSorting = hovered;
            RefreshSubElementsPositions();
        }

        public void RefreshSubElementsPositions() => 
            _itemImage.transform.localPosition = Vector3.back * (IsHovered ? .01f : 0.0035f) / transform.lossyScale.y;

        public void Dispose() => 
            Destroy(gameObject);
    }
}