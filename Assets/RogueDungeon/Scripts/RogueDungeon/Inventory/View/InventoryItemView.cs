using Common.UtilsDotNet;
using Common.UtilsUnity;
using Inventory.Shared;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Inventory.View
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _shadow;
        [SerializeField] private Color _legalPositionShadowColor;
        [SerializeField] private Color _illegalPositionShadowColor;

        private IItemViewInfo _itemViewInfo;
        private bool _isBeingPointedAt;
        private bool _isBeingDragged;

        public Vector2 Size => ((RectTransform)transform).sizeDelta;
        public bool IsBeingDragged => _isBeingDragged;
        public Vector3 WorldPosition => _itemImage.transform.position - Vector3.up * GetVerticalOffset();
        public int InstanceId => _itemViewInfo.InstanceId;
        public Vector2Int SizeInCells => _itemViewInfo.Size;

        [Inject]
        private void Construct(IItemViewInfo item)
        {
            _itemViewInfo = item;
            _itemImage.sprite = item.Sprite;
            _shadow.sprite = item.Sprite;
            UpdateVerticalOffset();
        }

        public bool IsEquippableIntoSlotType(SlotType slotType) => 
            _itemViewInfo.IsEquippableIntoSlotType(slotType);

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
            var containerSize = (Vector2)_itemViewInfo.Size * value;
            ((RectTransform)transform).sizeDelta = containerSize;
            
            var spriteAspect = _itemImage.sprite.rect.AspectRatio();

            var finalSize = spriteAspect > containerSize.AspectRatio()
                ? new Vector2(containerSize.x, containerSize.x / spriteAspect) 
                : new Vector2(containerSize.y * spriteAspect, containerSize.y);
            
            _itemImage.rectTransform.sizeDelta = finalSize;
            _shadow.rectTransform.sizeDelta = finalSize;
        }

        public void SetIsBeingPointedAt(bool value)
        {
            _isBeingPointedAt = value;
            UpdateVerticalOffset();
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
    }
}