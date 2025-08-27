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
        [SerializeField] private Color _legalPositionShadowColor;
        [SerializeField] private Color _illegalPositionShadowColor;
        [SerializeField] private ItemProjection _projection;

        private bool _isBeingDragged;

        private IItemViewSetupArgs _itemViewSetupArgs;
        public string Id => _itemViewSetupArgs.Id;
        public bool IsHovered { get; private set; }

        public IProjectionView ProjectionView => _projection;
        
        public void Setup(IItemViewSetupArgs itemViewSetupArgs)
        {
            _itemViewSetupArgs = itemViewSetupArgs;
            _itemImage.sprite = _itemViewSetupArgs.Sprite;
            _projection.SetSprite(itemViewSetupArgs.Sprite);
            UpdateVerticalOffset();
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent, worldPositionStays: false);
            // parent scale will affect the offset
            UpdateVerticalOffset();
        }

        public void SetLocalPosition(Vector2 pos) => 
            // because the canvas is rotated 90 degrees
            transform.localPosition = new Vector3(pos.x, 0, pos.y);

        public Vector2 GetScreenPosition(Camera camera) => 
            camera.WorldToScreenPoint(transform.position);

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
            UpdateVerticalOffset();
        }

        public void DisplayBeingDragged(bool beingDragged)
        {
            _isBeingDragged = beingDragged;
            UpdateVerticalOffset();
        }

        private void UpdateVerticalOffset()
        {
            var offset = .0035f;
            if (IsHovered || _isBeingDragged)
                offset += .0065f;
            _itemImage.transform.localPosition = Vector3.back * offset / transform.lossyScale.y;
        }
    }
}