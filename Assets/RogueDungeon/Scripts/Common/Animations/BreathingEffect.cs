using UnityEngine;

namespace Common.Animations
{
    public class BreathingEffect : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _amplitude = 0.05f; 
        [SerializeField] private float _frequency = 1.0f;
        [SerializeField] private float _horizontalRatio = .25f;

        private Vector3 _startPosition;

        private void Start() => 
            _startPosition = transform.localPosition;

        private void Update()
        {
            var verticalOffset = Mathf.Sin(Time.time * _frequency + _offset) * _amplitude;
            var horizontalOffset = Mathf.Sin(Time.time * _frequency * _horizontalRatio + _offset) * (_amplitude * 0.5f);
            transform.localPosition = _startPosition + new Vector3(horizontalOffset, verticalOffset, 0);
        }
    }
}