using Common.Fsm;
using RogueDungeon.Items;
using RogueDungeon.Player.Model.Attacks;
using Zenject;
using ITickable = Common.Lifecycle.ITickable;

namespace RogueDungeon.Player.Model.Behaviours.Hands
{
    public class PlayerHandBehaviour : ITickable
    {
        private readonly IFactory<IItem, HandHeldItemPresenter> _presenterFactory;
        private readonly ItemMoveSetFactory _moveSetFactory;

        private IItem _currentItem;
        private IItem _intendedMainHandItem;

        private HandHeldItemPresenter _itemPresenter;
        private StateMachine _itemMoveSet;

        public IItem CurrentItem
        {
            get => _currentItem;
            set
            {
                _currentItem = value;
                if (_currentItem != null)
                {
                    _itemPresenter = _presenterFactory.Create(_currentItem);
                    CreateItemMoveSet();
                }
                else
                {
                    _itemPresenter.Release();
                    _itemPresenter = null;
                    DeleteItemMoveSet();
                }
            }
        }
        public IItem IntendedItem { get; set; }
        public bool IsIdle => _currentItem is null || _itemMoveSet.CurrentState is ItemIdleMove;

        public PlayerHandBehaviour(IFactory<IItem, HandHeldItemPresenter> presenterFactory, ItemMoveSetFactory moveSetFactory)
        {
            _presenterFactory = presenterFactory;
            _moveSetFactory = moveSetFactory;
        }
        
        private void CreateItemMoveSet()
        {
            _moveSetFactory.BindItem(_currentItem);
            _itemMoveSet = _moveSetFactory.Create(_currentItem.Config);
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
            if(CurrentItem == null && IntendedItem != null)
                CurrentItem = IntendedItem; 
        }
    }
}