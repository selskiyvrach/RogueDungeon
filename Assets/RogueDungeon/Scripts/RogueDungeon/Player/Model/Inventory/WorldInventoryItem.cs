using UnityEngine;

namespace RogueDungeon.Player.Model.Inventory
{
    public class WorldInventoryItem : MonoBehaviour
    {
        private Vector3 _normalScale;
        private bool _isPointedAtBackingField;

        public bool IsPointedAt
        {
            get => _isPointedAtBackingField;
            set
            {
                if(value == _isPointedAtBackingField)
                    return;
                
                _isPointedAtBackingField = value;
                transform.localScale = _isPointedAtBackingField ? _normalScale * 1.2f : _normalScale;
            }
        }

        public bool IsBeingDragged { get; set; }

        private void Awake() => 
            _normalScale = transform.localScale;
    }
}