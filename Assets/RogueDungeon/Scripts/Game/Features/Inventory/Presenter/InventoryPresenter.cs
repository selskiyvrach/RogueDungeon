using System.Collections.Generic;
using Game.Features.Inventory.Domain;
using Game.Features.Inventory.Shared;
using Game.Features.Inventory.View;
using Game.Features.Levels;
using Game.Features.Levels.Domain;

namespace Game.Features.Inventory.Presenter
{
    public class InventoryPresenter : IInventoryInteractor, IInventoryInfoProvider
    {
        private readonly Level _level;
        private Domain.Inventory _model;
        private InventoryView _view;
        private readonly WorldInventoryItemFactory _itemFactory;

        private readonly Dictionary<SlotType, InventoryItemView> _slotItemsCache = new();

        // late initialization since model and view depend on the this. interfaces
        public void Construct(Domain.Inventory model, InventoryView view)
        {
            _model = model;
            _view = view;
            _view.Hide();
        }

        // model's handle to open inventory
        void IInventoryInteractor.OpenInventory() => 
            _view.Show(_level.CurrentRoom.GameObject.LootArea.GetComponent<ItemContainer>(), this);

        // model's handle to close inventory
        void IInventoryInteractor.CloseInventory() => 
            _view.Hide();

        // view's handle to get items
        InventoryItemView IInventoryInfoProvider.GetSlotItem(SlotType slotType)
        {
            if (_slotItemsCache.TryGetValue(slotType, out var itemView))
                return itemView;
                    
            if(_model.GetItem(slotType) is not {} item)
                return null;
            
            return _slotItemsCache[slotType] = _itemFactory.Create(item);
        }
    }
}