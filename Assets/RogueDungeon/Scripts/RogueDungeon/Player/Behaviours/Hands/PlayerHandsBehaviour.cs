using Common.Fsm;
using RogueDungeon.Items;
using Zenject;

namespace RogueDungeon.Player.Behaviours.Hands
{
    public class PlayerHandsBehaviour : IHandheldContext
    {
        private readonly HandHeldContext _handHeldContext;
        private readonly StateMachine _unsheathMoveSet;
        private readonly IFactory<IItem, HandHeldItemPresenter> _presenterFactory;
        private readonly ItemMoveSetFactory _moveSetFactory;
        
        private Item _currentItem;
        private Item _intendedItem;
        private HandHeldItemPresenter _itemPresenter;
        private StateMachine _itemMoveSet;
        
        Item IHandheldContext.CurrentItem
        {
            get => _handHeldContext.CurrentItem;
            set => _handHeldContext.CurrentItem = value;
        }

        Item IHandheldContext.IntendedItem
        {
            get => _handHeldContext.IntendedItem;
            set => _handHeldContext.IntendedItem = value;
        }

        public PlayerHandsBehaviour(HandHeldContext handHeldContext, StateMachine unsheathMoveSet, IFactory<IItem, HandHeldItemPresenter> presenterFactory, ItemMoveSetFactory moveSetFactory)
        {
            _handHeldContext = handHeldContext;
            _unsheathMoveSet = unsheathMoveSet;
            _presenterFactory = presenterFactory;
            _moveSetFactory = moveSetFactory;
        }

        void IHandheldContext.SetCurrentItemInteractable(bool value) => 
            _handHeldContext.SetCurrentItemInteractable(value);

        public void Initialize()
        {
            if(!_handHeldContext.Inited)
                _handHeldContext.Init(() => _currentItem, () => _intendedItem, SetCurrentItem, n => _intendedItem = n, SetItemMoveSetActive);
            _unsheathMoveSet.Initialize();
        }

        private void SetCurrentItem(Item value)
        {
            _currentItem = value;
            if (_currentItem != null)
                _itemPresenter = _presenterFactory.Create(_currentItem);
            else
            {
                _itemPresenter.Release();
                _itemPresenter = null;
                DeleteItemMoveSet();
            }
        }

        public void SetItemMoveSetActive(bool value)
        {
            if(value)
                CreateItemMoveSet();
            else
                DeleteItemMoveSet();
        }

        private void CreateItemMoveSet()
        {
            _moveSetFactory.BindItem(_currentItem);
            _itemMoveSet = _moveSetFactory.Create(_currentItem.MoveSetConfig);
            _itemMoveSet.Initialize();
        }

        private void DeleteItemMoveSet()
        {
            _moveSetFactory.Unbind();
            _itemMoveSet = null;
        }

        public void Tick(float deltaTime)
        {
            _itemMoveSet?.Tick(deltaTime);
            _unsheathMoveSet.Tick(deltaTime);
        }
    }
}