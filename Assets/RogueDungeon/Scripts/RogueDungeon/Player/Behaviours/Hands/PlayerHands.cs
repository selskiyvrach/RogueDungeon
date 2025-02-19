using Common.MoveSets;
using RogueDungeon.Items;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHands : IHandheldContext
    {
        private readonly IFactory<ItemConfig, HandHeldItemPresenter> _presenterFactory;
        private readonly IFactory<MoveSetConfig, MoveSetBehaviour> _moveSetFactory;
        
        private Item _currentItem;
        private HandHeldItemPresenter _itemPresenter;
        private MoveSetBehaviour _itemMoveSet;

        public Item IntendedItem { get; set; }

        public Item CurrentItem
        {
            get => _currentItem;
            set
            {
                _currentItem = value;
                if (_currentItem != null)
                    _itemPresenter = _presenterFactory.Create(_currentItem.Config);
                else
                {
                    _itemPresenter.Release();
                    _itemPresenter = null;
                    DeleteMoveSet();
                }
            }
        }

        public PlayerHands(IFactory<ItemConfig, HandHeldItemPresenter> presenterFactory, IFactory<MoveSetConfig, MoveSetBehaviour> moveSetFactory)
        {
            _presenterFactory = presenterFactory;
            _moveSetFactory = moveSetFactory;
        }

        public void SetCurrentItemInteractable(bool value)
        {
            if(value)
                CreateMoveSet();
            else
                DeleteMoveSet();
        }

        private void CreateMoveSet()
        {
            _itemMoveSet = _moveSetFactory.Create(CurrentItem.Config.MoveSetConfig);
            _itemMoveSet.Enable();
        }

        private void DeleteMoveSet()
        {
            _itemMoveSet?.Disable();
            _itemMoveSet = null;
        }
    }
}