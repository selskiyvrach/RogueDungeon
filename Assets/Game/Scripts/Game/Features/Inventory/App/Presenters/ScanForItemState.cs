using UnityEngine.Assertions;

namespace Game.Features.Inventory.App.Presenters
{
    public class ScanForItemState
    {
        private readonly ElementsRegistry _registry;
        private readonly Mediator _mediator;
        
        private ItemPresenter _currentItem;

        public ScanForItemState(Mediator mediator, ElementsRegistry registry)
        {
            _mediator = mediator;
            _registry = registry;
        }

        public void Enter()
        {
            foreach (var item in _registry.Items) 
                item.EnableRaycasts();
        }

        public void OnItemHovered(ItemPresenter item)
        {
            Assert.IsNull(_currentItem);
            Assert.IsNotNull(item);
            _currentItem = item;
            _currentItem.DisplayHovered();
        }

        public void OnItemUnhovered(ItemPresenter item)
        {
            Assert.IsNotNull(_currentItem);
            Assert.AreEqual(_currentItem, item);
            _currentItem.DisplayUnhovered();
            _currentItem = null;
        }

        public void OnPointerDown()
        {
            if (_currentItem == null)
                return;
            
            foreach (var item in _registry.Items) 
                item.DisableRaycasts();
            
            _mediator.StartCarryingItem(_currentItem);
        }
    }
}