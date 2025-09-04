using System;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField] private WorldInventoryAnimator _animator;

        [Inject]
        private void Construct(Camera gameCamera) => 
            _inventoryCanvas.worldCamera = gameCamera;

        public void Show() => 
            _animator.PlayUnpack();

        public void Hide(Action callback) => 
            _animator.PlayPack(callback);
        
        public void Destroy() => 
            Destroy(gameObject);
    }
}