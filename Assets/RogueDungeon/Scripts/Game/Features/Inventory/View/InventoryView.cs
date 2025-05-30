using Game.Libs.Camera;
using Game.Libs.Input;
using UnityEngine;
using Zenject;

namespace Game.Features.Inventory.View
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Canvas _inventoryCanvas;
        [SerializeField] private RectTransform _inventoryRect;
        [SerializeField, HideInInspector] private ItemContainer[] _containers;
        
        private InventoryItemView _currentInventoryItem;
        private IGameCamera _camera;
        private IPlayerInput _input;
        private ItemContainer _lootArea;
        private IInventoryInfoProvider _inventoryInfo;
        private ItemContainer _currentItemContainer;

        [Inject]
        public void Construct(IPlayerInput input, IGameCamera gameCamera)
        {
            _input = input;
            _camera = gameCamera;
            _inventoryCanvas.worldCamera = _camera.Camera;
        }

        private void OnValidate() => 
            _containers = GetComponentsInChildren<ItemContainer>();

        public void Show(ItemContainer lootArea, IInventoryInfoProvider inventoryInfo)
        {
            gameObject.SetActive(true);
            _lootArea = lootArea;
            _inventoryInfo = inventoryInfo;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Tick(float timeDelta)
        {
         
        }
    }
}