using Game.Features.Inventory.App.Presenters;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class ItemProjection : MonoBehaviour, IProjectionView
    {
        [SerializeField] private Image _image;
        
        public void SetSprite(Sprite sprite) => 
            _image.sprite = sprite;

        public void SetPosition(Vector3 worldPosition) => 
            transform.position = worldPosition;

        public void SetIsValid(bool isValid) => 
            _image.color = isValid ? Color.black : Color.red;
    }
}