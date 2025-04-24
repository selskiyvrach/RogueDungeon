using System;
using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _item;
        [SerializeField] private SpriteRenderer _shadow;
        
        private bool _isPointedAtBackingField;
        private bool _isBeingDraggedBackingField;

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
                transform.position = ProjectedPosition;
                _item.transform.localPosition = Vector3.back * GetVerticalOffset();
                _shadow.transform.localPosition = Vector3.zero;
            }
        }

        private void Start() => 
            _item.transform.localPosition = Vector3.back * GetVerticalOffset();

        private float GetVerticalOffset()
        {
            var offset = .0025f;
            if (IsPointedAt || IsBeingDragged)
                offset += .0025f;
            return offset;
        }
    }
}