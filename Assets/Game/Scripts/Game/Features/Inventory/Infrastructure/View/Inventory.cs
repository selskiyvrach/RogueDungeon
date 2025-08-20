using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField, HideInInspector] private Container[] _containers;
        
        [Inject]
        public void Construct(Camera gameCamera) => 
            _inventoryCanvas.worldCamera = gameCamera;

        private void OnValidate() => 
            _containers = GetComponentsInChildren<Container>(true);
    }
}