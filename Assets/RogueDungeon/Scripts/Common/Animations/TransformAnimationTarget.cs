using UnityEngine;

namespace Common.Animations
{
    public class TransformAnimationTarget : MonoBehaviour
    {
        private Vector3 _localRotation;
        
        private void Awake() => 
            _localRotation = transform.localRotation.eulerAngles;

        public Vector3 LocalPosition
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }

        public Vector3 LocalRotation
        {
            get => _localRotation;
            set
            {
                // need to store set value so it's not lost for read due to quaternion conversions
                _localRotation = value;
                transform.localRotation = Quaternion.Euler(_localRotation);
            }
        }
    }
}