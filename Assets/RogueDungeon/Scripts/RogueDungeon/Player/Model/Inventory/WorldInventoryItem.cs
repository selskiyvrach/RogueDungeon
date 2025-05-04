using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _item;
        [SerializeField] private SpriteRenderer _shadow;
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
                _item.transform.localPosition = Vector3.back * GetVerticalOffset();
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
                _item.transform.localPosition = Vector3.back * GetVerticalOffset();
                
                if(_isBeingDraggedBackingField)
                    return;

                transform.position = _lastLegalPosition;
                _shadow.transform.localPosition = Vector3.zero;
                IsCurrentPositionLegal = true;
            }
        }

        private void Start() => 
            _item.transform.localPosition = Vector3.back * GetVerticalOffset();

        private float GetVerticalOffset()
        {
            var offset = .0025f;
            if (IsPointedAt || IsBeingDragged)
                offset += .005f;
            return offset;
        }
    }
}