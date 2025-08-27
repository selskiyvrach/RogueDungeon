using System;
using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class ItemProjection : MonoBehaviour, IProjectionView
    {
        [SerializeField] private Image _image;
        [SerializeField] private Color _normalColor;
        [SerializeField] private Color _invalidColor;

        private bool _isValid = true;

        private void Start() => 
            UpdateColor();

        public void SetSprite(Sprite sprite) => 
            _image.sprite = sprite;

        public void SetPosition(Vector3 worldPosition) => 
            transform.position = worldPosition;
        
        public void SetSize(Vector2 size) =>
            _image.rectTransform.sizeDelta = size;

        public void SetIsValid(bool isValid)
        {
            _isValid = isValid;
            UpdateColor();
        }

        private void UpdateColor() => 
            _image.color = _isValid ? _normalColor : _invalidColor;
    }
}