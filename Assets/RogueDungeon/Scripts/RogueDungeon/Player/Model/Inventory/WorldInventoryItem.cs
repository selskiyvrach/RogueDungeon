using Common.Unity;
using Common.UtilsDotNet;
using RogueDungeon.Items;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryItem : MonoBehaviour
    {
        [SerializeField] private Image _item;
        [SerializeField] private Image _shadow;
        [SerializeField] private Color _legalPositionShadowColor;
        [SerializeField] private Color _illegalPositionShadowColor;
        
        private bool _isPointedAtBackingField;
        private bool _isBeingDraggedBackingField;
        private bool _isCurrentPositionLegalBackingField;
        private Vector3 _lastLegalPosition;

        public Vector3 Position
        {
            get => _item.transform.position - Vector3.up * GetVerticalOffset();
            set => _item.transform.position = value + Vector3.up * GetVerticalOffset();
        }

        public Vector3 ProjectedPosition
        {
            get => _shadow.transform.position;
            set => _shadow.transform.position = value;
        }

        public PlaceablePlace LastLegalPlace { get; set; }

        public bool IsCurrentPositionLegal
        {
            get => _isCurrentPositionLegalBackingField;
            set
            {
                if(value)
                    _lastLegalPosition = ProjectedPosition;
                
                if(value == _isCurrentPositionLegalBackingField)
                    return;
                
                _isCurrentPositionLegalBackingField = value;
                _shadow.color = value ? _legalPositionShadowColor : _illegalPositionShadowColor;
            }
        }

        public bool IsPointedAt
        {
            get => _isPointedAtBackingField;
            set
            {
                if(value == _isPointedAtBackingField)
                    return;
                
                _isPointedAtBackingField = value;
                _item.transform.localPosition = Vector3.back * GetVerticalOffset() / transform.lossyScale.y;
            }
        }

        public bool IsBeingDragged
        {
            get => _isBeingDraggedBackingField;
            set
            {
                if(_isBeingDraggedBackingField == value)
                    return;
                _isBeingDraggedBackingField = value;
                _item.transform.localPosition = Vector3.back * GetVerticalOffset()/ transform.lossyScale.y;

                if (_isBeingDraggedBackingField)
                    return;

                transform.SetParent(LastLegalPlace.transform);
                transform.position = _lastLegalPosition;
                _shadow.transform.localPosition = Vector3.zero;
                IsCurrentPositionLegal = true;
            }
        }

        // [Inject]
        [Button]
        private void Construct(ItemConfig config)
        {
            var sprite = config.Sprite;
            _item.sprite = sprite;
            _shadow.sprite = sprite; 
            
            var containerSize = (Vector2)config.Size * 50;
            ((RectTransform)transform).sizeDelta = containerSize;
            
            var spriteAspect = sprite.texture.AspectRatio();

            var finalSize = spriteAspect > containerSize.AspectRatio()
                ? new Vector2(containerSize.x, containerSize.x / spriteAspect) 
                : new Vector2(containerSize.y * spriteAspect, containerSize.y);
            
            _item.rectTransform.sizeDelta = finalSize;
            _shadow.rectTransform.sizeDelta = finalSize;
        }

        private void Start() => 
            _item.transform.localPosition = Vector3.back * GetVerticalOffset() / transform.lossyScale.y;

        public void SetRotation(float rotation) => 
            transform.localRotation = Quaternion.Euler(0, 0, rotation);

        private float GetVerticalOffset()
        {
            var offset = .0025f;
            if (IsPointedAt || IsBeingDragged)
                offset += .005f;
            return offset;
        }
    }
}