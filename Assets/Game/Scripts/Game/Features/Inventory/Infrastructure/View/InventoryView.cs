 using Game.Features.Inventory.App.Presenters;
 using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.Infrastructure.View
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField, HideInInspector] private ItemContainer[] _containers;
        
        private InventoryItemView _currentInventoryItem;
        private ItemContainer _lootArea;
        private ItemContainer _currentItemContainer;

        [Inject]
        public void Construct(Camera gameCamera) => 
            _inventoryCanvas.worldCamera = gameCamera;

        private void OnValidate() => 
            _containers = GetComponentsInChildren<ItemContainer>();

        public void Show(ItemContainer lootArea)
        {
            gameObject.SetActive(true);
            _lootArea = lootArea;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Tick(float timeDelta)
        {
         
        }

        public void Dispose() => 
            Destroy(gameObject);
    }
}