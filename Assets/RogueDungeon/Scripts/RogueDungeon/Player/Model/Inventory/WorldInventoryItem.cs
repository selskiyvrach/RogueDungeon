using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryItem : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _shadow;
        
        private bool _isPointedAtBackingField;
        private bool _isBeingDragged;

        public Vector2 Position
        {
            get => new(transform.localPosition.x, transform.localPosition.z);
            set
            {
                var offset = GetVerticalOffset();
                transform.localPosition = new Vector3(value.x, offset, value.y);
                // since the item itself is rotated and shadow is a child
                _shadow.transform.localPosition = Vector3.back * - offset;
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
                Position = Position;
            }
        }

        public bool IsBeingDragged
        {
            get => _isBeingDragged;
            set
            {
                if(_isBeingDragged == value)
                    return;
                _isBeingDragged = value;
                Position = Position;
            }
        }

        private float GetVerticalOffset()
        {
            var offset = 0.001f;
            if (IsPointedAt)
                offset += .01f;
            if(IsBeingDragged)
                offset += .01f;
            return offset;
        }
    }
}