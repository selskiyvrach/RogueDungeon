using Common.Unity;
using Common.UtilsDotNet;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace RogueDungeon.Player.Model.Inventory
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private Image _shadow;
        [SerializeField] private Color _legalPositionShadowColor;
        [SerializeField] private Color _illegalPositionShadowColor;

        private IItemViewModel _item;
        private bool _isBeingPointedAt;
        private bool _isBeingDragged;

        public Vector2 Size => ((RectTransform)transform).sizeDelta;
        public bool IsBeingDragged => _isBeingDragged;
        public Vector3 WorldPosition => _itemImage.transform.position - Vector3.up * GetVerticalOffset();

        [Inject]
        private void Construct(IItemViewModel item)
        {
            _item = item;
            _itemImage.sprite = item.Sprite;
            _shadow.sprite = item.Sprite; 
            UpdateVerticalOffset();
        }

        public void SetPosition(Vector3 value) => 
            _itemImage.transform.position = value + Vector3.up * GetVerticalOffset();

        public void SetProjectionPosition(Vector3 value) => 
            _shadow.transform.position = value;

        public void SetIsCurrentPositionLegal(bool value) => 
            _shadow.color = value ? _legalPositionShadowColor : _illegalPositionShadowColor;

        public void SetIsBeingDragged(bool value)
        {
            _isBeingDragged = value;
            UpdateVerticalOffset();
        }

        public void SetParent(IItemParent parent)
        {
            transform.SetParent(parent.ParentObject, worldPositionStays: true);
            SetCellSize(parent.CellSize);
        }

        private void SetCellSize(float value)
        {
            var containerSize = (Vector2)_item.Size * value;
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