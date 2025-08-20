using Game.Features.Inventory.App.Presenters;
using Game.Libs.UI;
using Libs.Utils.Unity;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class ItemView : HoverableGraphic, IItemView
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _shadow;
        [SerializeField] private Color _legalPositionShadowColor;
        [SerializeField] private Color _illegalPositionShadowColor;

        private bool _isBeingPointedAt;
        private bool _isBeingDragged;
        
        private IItemInfo _itemInfo;
        public string Id => _itemInfo.Id;
        public void Setup(IItemInfo itemInfo)
        {
            _itemInfo = itemInfo;
            _itemImage.sprite = _itemInfo.Sprite;
            _shadow.sprite = _itemInfo.Sprite;
            UpdateVerticalOffset();
        }

        public void SetPosition(Vector3 value) => 
            _itemImage.transform.position = value + Vector3.up * GetVerticalOffset();

        public void SetupProjection(Vector3 pos, bool isPositionLegal)
        {
            _shadow.transform.position = pos;
            _shadow.color = isPositionLegal ? _legalPositionShadowColor : _illegalPositionShadowColor;
        }

        public void SetIsBeingDragged(bool value)
        {
            _isBeingDragged = value;
            UpdateVerticalOffset();
        }

        public void SetParent(Transform parent) => 
            transform.SetParent(parent, worldPositionStays: true);

        public void SetCellSize(float value)
        {
            var containerSize = (Vector2)_itemInfo.Size * value;
            ((RectTransform)transform).sizeDelta = containerSize;
            
            var spriteAspect = _itemImage.sprite.rect.AspectRatio();

            var finalSize = spriteAspect > containerSize.AspectRatio()
                ? new Vector2(containerSize.x, containerSize.x / spriteAspect) 
                : new Vector2(containerSize.y * spriteAspect, containerSize.y);
            
            _itemImage.rectTransform.sizeDelta = finalSize;
            _shadow.rectTransform.sizeDelta = finalSize;
        }

        private void UpdateVerticalOffset() => 
            _itemImage.transform.localPosition = Vector3.back * GetVerticalOffset() / transform.lossyScale.y;

        private float GetVerticalOffset()
        {
            var offset = .0035f;
            if (_isBeingPointedAt || _isBeingDragged)
                offset += .0065f;
            return offset;
        }

        public void DisplayHovered()
        {
            _isBeingPointedAt = true;
            UpdateVerticalOffset();
        }

        public void DisplayUnhovered()
        {
            _isBeingPointedAt = false;
            UpdateVerticalOffset();
        }

        public void DisplayBeingDragged()
        {
            _isBeingDragged = true;
            UpdateVerticalOffset();
        }

        public void DisplayNotBeingDragged()
        {
            _isBeingDragged = false;
            UpdateVerticalOffset();
        }
    }
}