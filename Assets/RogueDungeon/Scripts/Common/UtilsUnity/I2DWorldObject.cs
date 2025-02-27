using UnityEngine;

namespace Common.Unity
{
    public interface ITwoDWorldObject
    {
        void SetParent(Transform parent);
        Vector2 LocalPosition { get; set; }
        Vector2 Rotation { get; set; }
    }
    
    public class TwoDWorldObject : ITwoDWorldObject
    {
        private readonly GameObject _gameObject;

        public Vector2 LocalPosition
        {
            get
            {
                var pos = _gameObject.transform.localPosition;
                return new Vector2(pos.x, pos.z);
            }
            set => _gameObject.transform.localPosition = new Vector3(value.x, 0, value.y);
        }

        public Vector2 Rotation
        {
            get
            {
                var dir = _gameObject.transform.forward;
                return new Vector2(dir.x, dir.z);
            }
            set => _gameObject.transform.forward = new Vector3(value.x, 0, value.y);
        }
        
        public TwoDWorldObject(GameObject gameObject) => 
            _gameObject = gameObject;

        public void SetParent(Transform parent) => 
            _gameObject.transform.SetParent(parent);
    }
}